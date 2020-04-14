using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Api.Models;
using TitanGate.WebSiteStore.Entities;

namespace TitanGate.WebSiteStore.Api.Mappers
{
    public class SearchObjectMapper : IMapper<SearchObjectModel, WebSiteSearchObject>
    {
        public SearchObjectModel EntityToModel(WebSiteSearchObject entity)
        {
            throw new NotImplementedException();
        }

        public WebSiteSearchObject ModelToEntity(SearchObjectModel model)
        {
            throw new NotImplementedException();
        }
    }
}
