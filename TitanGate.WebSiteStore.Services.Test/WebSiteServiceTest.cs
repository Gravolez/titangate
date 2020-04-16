using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Repository;

namespace TitanGate.WebSiteStore.Services.Test
{
    [TestClass]
    public class WebSiteServiceTest
    {
        private ServiceCollection _services;
        private IConfiguration _configuration;
        private Mock<IWebSiteRepository> _webSiteRepositoryMock;
        private Mock<IRepositorySession> _repositorySessionMock;
        private ServiceProvider _serviceProvider;

        [TestInitialize]
        public void InitTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            _services = new ServiceCollection();
            _services.Configure<AppSettings>(_configuration);
            _services.AddWebStoreServices();

            _webSiteRepositoryMock = new Mock<IWebSiteRepository>();
            _services.AddTransient<IWebSiteRepository>((serviceProvicer) => _webSiteRepositoryMock.Object);

            _repositorySessionMock = new Mock<IRepositorySession>();
            _services.AddTransient<IRepositorySession>((serviceProvicer) => _repositorySessionMock.Object);

            _serviceProvider = _services.BuildServiceProvider();
        }

        [TestMethod]
        public async Task DownloadFile__Returns_file()
        {
            // arrange 
            int webSiteId = 12345;
            var website = new WebSite
            {
                HasScreenshot = true,
                ScreenshotExt = ".jpg"
            };

            _webSiteRepositoryMock.Setup(x => x.FindById(webSiteId)).Returns(Task.FromResult(website)).Verifiable();

            // act
            var webSiteService = _serviceProvider.GetService<IWebSiteService>();
            var result = await webSiteService.DownloadFile(webSiteId);

            // assert
            var fileBytes = await File.ReadAllBytesAsync(Path.Combine(
                _configuration["BaseAppFolder"], 
                _configuration["BaseFilesFolder"],
                Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                "01\\23\\45.jpg"));
            CollectionAssert.AreEqual(fileBytes, result.contents);
            Assert.AreEqual(website.ScreenshotExt, result.extension);
            _webSiteRepositoryMock.Verify();
        }

        [TestMethod]
        public async Task UploadFile__Uploads_file()
        {
            // arrange
            int webSiteId = 54321;
            var fileBytes = await File.ReadAllBytesAsync(Path.Combine(
                _configuration["BaseAppFolder"],
                _configuration["BaseFilesFolder"],
                Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                "01\\23\\45.jpg"));
            var extension = ".jpg";

            var website = new WebSite
            {
                HasScreenshot = false,
                ScreenshotExt = null
            };

            WebSite savedWebsite = null;

            _webSiteRepositoryMock.Setup(x => x.FindById(webSiteId)).Returns(Task.FromResult(website)).Verifiable();
            _webSiteRepositoryMock
                .Setup(x => x.Update(website))
                .Callback((WebSite w) => savedWebsite = w).Returns(Task.FromResult(true)).Verifiable();


            var savedFilePath = Path.Combine(
                    _configuration["BaseAppFolder"],
                    _configuration["BaseFilesFolder"],
                    Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                    "05\\43\\21.jpg");
            try
            {
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                _repositorySessionMock.Setup(x => x.BeginWork()).Returns(mockUnitOfWork.Object).Verifiable();
                mockUnitOfWork.Setup(x => x.Persist()).Verifiable();

                // act
                var webSiteService = _serviceProvider.GetService<IWebSiteService>();
                await webSiteService.UploadFile(webSiteId, fileBytes, extension);

                // assert
                var content = await File.ReadAllBytesAsync(savedFilePath);
                CollectionAssert.AreEqual(content, fileBytes);
                _webSiteRepositoryMock.Verify();
                _repositorySessionMock.Verify();
                mockUnitOfWork.Verify();
                Assert.IsNotNull(savedWebsite);
                Assert.AreEqual(true, savedWebsite.HasScreenshot);
                Assert.AreEqual(extension, savedWebsite.ScreenshotExt);
            }
            finally
            {
                if (File.Exists(savedFilePath))
                {
                    File.Delete(savedFilePath);
                    Directory.Delete(Path.GetDirectoryName(savedFilePath));
                }
            }
        }
    }
}
