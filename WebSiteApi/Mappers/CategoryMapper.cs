using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities.DB;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public class CategoryMapper : IMapper<CategoryModel, WebSiteCategory>
    {
        public CategoryModel EntityToModel(WebSiteCategory entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public WebSiteCategory ModelToEntity(CategoryModel model)
        {
            return new WebSiteCategory(model.Id, model.Name);
        }
    }
}
