using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class SearchObjectModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public SortField[] SortFields { get; set; }
    }
}
