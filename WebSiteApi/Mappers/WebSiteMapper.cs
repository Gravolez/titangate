using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Entities.Exceptions;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public class WebSiteMapper: IMapper<WebSiteModel, WebSite>
    {
        private readonly IMapper<CategoryModel, WebSiteCategory> _categoryMapper;
        private readonly ICryptoService _cryptoService;
        private readonly AppSettings _settings;

        public WebSiteMapper(IMapper<CategoryModel, WebSiteCategory> categoryMapper, ICryptoService cryptoService, IOptions<AppSettings> settings)
        {
            _categoryMapper = categoryMapper;
            _cryptoService = cryptoService;
            _settings = settings.Value;
        }

        public WebSiteModel EntityToModel(WebSite webSiteEntity)
        {
            return new WebSiteModel
            {
                Name = webSiteEntity.Name,
                Url = webSiteEntity.Url,
                Category = _categoryMapper.EntityToModel(webSiteEntity.Category),
                Id = webSiteEntity.Id,
                Email = webSiteEntity.Email,
                Password = _cryptoService.Decrypt(webSiteEntity.Password),
                // ScreenshotUrl = webSiteEntity.Screenshot
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
                Email = model.Email,
                Password = _cryptoService.Encrypt(model.Password)
            };
        }
    }
}
