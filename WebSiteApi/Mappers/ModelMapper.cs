using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities;
using TitanGate.WebSiteStore.Entities.DB;
using TitanGate.WebSiteStore.Entities.Exceptions;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public class ModelMapper: IMapper<WebSiteModel, WebSite>
    {
        public WebSiteModel EntityToModel(WebSite webSiteEntity)
        {
            return new WebSiteModel
            {
                Name = webSiteEntity.Name,
                Url = webSiteEntity.Url,
                Category = (WebSiteCategoryEnum)webSiteEntity.Category.Id,
                Id = webSiteEntity.Id,
                Email = webSiteEntity.Login.Email,
                Password = webSiteEntity.Login.Password
            };
        }

        public WebSite ModelToEntity(WebSiteModel model)
        {
            return new WebSite
            {
                Name = model.Name,
                Url = model.Url,
                Id = model.Id,
                Category = model.Category switch
                {
                    WebSiteCategoryEnum.Porn => WebSiteCategory.Porn,
                    WebSiteCategoryEnum.Betting => WebSiteCategory.Betting,
                    WebSiteCategoryEnum.Entertainment => WebSiteCategory.Entertainment,
                    WebSiteCategoryEnum.Science => WebSiteCategory.Science,
                    _ => throw new WebSiteStoreException("No such value")
                },

            };
        }
    }
}
