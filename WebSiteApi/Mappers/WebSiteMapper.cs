using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public class WebSiteMapper: IMapper<WebSiteModel, WebSite>
    {
        private readonly IMapper<CategoryModel, WebSiteCategory> _categoryMapper;
        private readonly ICryptoService _cryptoService;

        public WebSiteMapper(
            IMapper<CategoryModel, WebSiteCategory> categoryMapper, 
            ICryptoService cryptoService)
        {
            _categoryMapper = categoryMapper;
            _cryptoService = cryptoService;
        }

        public WebSiteModel EntityToModel(WebSite webSiteEntity)
        {
            return new WebSiteModel
            {
                Name = webSiteEntity.Name,
                Url = webSiteEntity.Url,
                Category = _categoryMapper.EntityToModel(webSiteEntity.Category),
                Id = webSiteEntity.Id,
                Login = new LoginModel
                {
                    Email = webSiteEntity.Email,
                    Password = _cryptoService.Decrypt(webSiteEntity.Password),
                },
                ScreenshotUrl = webSiteEntity.HasScreenshot ? $"/api/website/{webSiteEntity.Id}/screenshot" : null
            };
        }

        public WebSite ModelToEntity(WebSiteModel model)
        {
            return new WebSite
            {
                Id = model.Id,
                Name = model.Name,
                Url = model.Url,
                Category = _categoryMapper.ModelToEntity(model.Category),
                Email = model.Login.Email,
                Password = _cryptoService.Encrypt(model.Login.Password)
            };
        }
    }
}
