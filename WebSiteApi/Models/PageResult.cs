using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    public class PagedResult
    {
        public IEnumerable<WebSiteModel> WebSites { get; set; }
        public int TotalCount { get; set; }
    }
}
