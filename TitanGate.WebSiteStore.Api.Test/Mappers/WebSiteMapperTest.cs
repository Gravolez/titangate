using Microsoft.VisualStudio.TestTools.UnitTesting;
using TitanGate.WebSiteStore.Api.Mappers;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Test
{
    [TestClass]
    public class WebSiteMapperTest
    {
        [TestMethod]
        public void ModelToEntity()
        {
            // arrange
            var model = new WebSiteModel
            {
                Category = new CategoryModel { Id = 2, Name = "Entertainment" },
                Name = "Name",
                Url = "https://www.google.com",
                ScreenshotUrl = "whatever",
                Login = new LoginModel
                {
                    Email = "totally@valid.email",
                    Password = "totally valid password"
                }
            };

            // act
            var mapper = new WebSiteMapper(new CategoryMapper(), new CryptoService());
            WebSite entity = mapper.ModelToEntity(model);

            // assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Category.Id, entity.Category.Id);
            Assert.AreEqual(model.Category.Name, entity.Category.Name);
            Assert.AreEqual(model.Url, entity.Url);
            Assert.AreEqual(model.Login.Email, entity.Email);
            Assert.AreNotEqual(model.Login.Password, entity.Password);
            Assert.AreEqual("Ef1PI4JaZxN6DJSS5MWInW8wOUJjcTscXy5SH9r9vnc=", entity.Password);
        }

        [TestMethod]
        public void EntityToModel()
        {
            // arrange
            var entity = new WebSite
            {
                Id = 1,
                HasScreenshot = true,
                ScreenshotExt = ".whatever",
                Category = new WebSiteCategory(2, "Entertainment"),
                Email = "totally@valid.email",
                Password = "Ef1PI4JaZxN6DJSS5MWInW8wOUJjcTscXy5SH9r9vnc=",
                Name = "Name",
                Url = "http://some.url" 
            };

            // act
            var mapper = new WebSiteMapper(new CategoryMapper(), new CryptoService());
            WebSiteModel model = mapper.EntityToModel(entity);

            // assert
            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Url, model.Url);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual("totally valid password", model.Login.Password);
            Assert.AreEqual(entity.Email, model.Login.Email);
            Assert.AreEqual(entity.Category.Name, model.Category.Name);
            Assert.AreEqual(entity.Category.Id, model.Category.Id);
            Assert.AreEqual("/api/website/1/screenshot", model.ScreenshotUrl);
        }

        [TestMethod]
        public void EntityToModel__When_has_no_screenshot__ScreenshotUrl_is_null()
        {
            // arrange
            var entity = new WebSite
            {
                Id = 1,
                HasScreenshot = false,
                ScreenshotExt = null,
                Category = new WebSiteCategory(2, "Entertainment"),
                Email = "totally@valid.email",
                Password = "Ef1PI4JaZxN6DJSS5MWInW8wOUJjcTscXy5SH9r9vnc=",
                Name = "Name",
                Url = "http://some.url"
            };

            // act
            var mapper = new WebSiteMapper(new CategoryMapper(), new CryptoService());
            WebSiteModel model = mapper.EntityToModel(entity);

            // assert
            Assert.IsNull(model.ScreenshotUrl);
        }
    }
}
