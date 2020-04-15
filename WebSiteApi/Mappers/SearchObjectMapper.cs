using System;
using System.Collections.Generic;
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
            var result = new WebSiteSearchObject
            {
                PageNumber = model.PageNumber,
                PageSize = model.PageSize,
                SortExpression = new List<(SortColumn, SortOrder)>()
            };

            foreach(var sort in model.SortFields)
            {
                result.SortExpression.Add(((SortColumn)sort.Field, (SortOrder)sort.Order));
            }

            return result;
        }
    }
}
