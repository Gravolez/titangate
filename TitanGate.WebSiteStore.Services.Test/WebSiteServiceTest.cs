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

        private readonly string _testImageFolder = "TestImages";

        public string BaseTestFiles
        {
            get
            {
                return Path.Combine(
                    _configuration["BaseAppFolder"],
                    _configuration["BaseFilesFolder"]);
            }
        }

        [TestInitialize]
        public void InitTest()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            _serviceProvider = ServiceTestHelper.InitProvider(services =>
            {
                services.Configure<AppSettings>(_configuration);
                _webSiteRepositoryMock = new Mock<IWebSiteRepository>();
                services.AddTransient<IWebSiteRepository>((x) => _webSiteRepositoryMock.Object);

                _repositorySessionMock = new Mock<IRepositorySession>();
                services.AddTransient<IRepositorySession>((x) => _repositorySessionMock.Object);
            });
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
                BaseTestFiles,
                Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                "01\\23\\45.jpg"));
            CollectionAssert.AreEqual(fileBytes, result.contents);
            Assert.AreEqual(website.ScreenshotExt, result.extension);
            _webSiteRepositoryMock.Verify();
        }

        [TestMethod]
        public async Task UploadFile__When_there_is_previous_screenshot__Deletes_previous_file()
        {
            // arrange
            int webSiteId = 62221;
            var fileBytes = await File.ReadAllBytesAsync(Path.Combine(
                BaseTestFiles,
                _testImageFolder,
                "test_image_1.jpeg"));
            var extension = ".jpeg";

            var website = new WebSite
            {
                HasScreenshot = true,
                ScreenshotExt = ".jpg"
            };

            WebSite savedWebsite = null;

            _webSiteRepositoryMock.Setup(x => x.FindById(webSiteId)).Returns(Task.FromResult(website)).Verifiable();
            _webSiteRepositoryMock
                .Setup(x => x.Update(website))
                .Callback((WebSite w) => savedWebsite = w).Returns(Task.FromResult(true)).Verifiable();
            
            string savedFilePath = Path.Combine(
                    BaseTestFiles,
                    Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                    "06\\22\\21.jpeg");

            string oldFilePath = Path.Combine(
                BaseTestFiles,
                Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                "06\\22\\21.jpg");

            string oldFilePathFolder = Path.GetDirectoryName(oldFilePath);
            if (!Directory.Exists(oldFilePathFolder))
            {
                Directory.CreateDirectory(oldFilePathFolder);
            }

            File.Copy(Path.Combine(
                BaseTestFiles,
                _testImageFolder,
                "test_image_2.jpg"), oldFilePath);

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
                Assert.IsFalse(File.Exists(oldFilePath));
            }
            finally
            {
                if (File.Exists(savedFilePath))
                {
                    File.Delete(savedFilePath);
                }
            }
        }

        [TestMethod]
        public async Task UploadFile__When_there_is_no_existing_screenshot_and_folder__Creates_missing_directory()
        {
            // arrange
            int webSiteId = 54321;
            var fileBytes = await File.ReadAllBytesAsync(Path.Combine(
                BaseTestFiles,
                _testImageFolder,
                "test_image_1.jpeg"));
            var extension = ".jpeg";

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


            string savedFilePath = Path.Combine(
                    BaseTestFiles,
                    Enum.GetName(typeof(FileCategoryEnum), FileCategoryEnum.WebsiteScreenshot),
                    "05\\43\\21.jpeg");

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
