using System;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class SortField
    {
        public SortFieldEnum Field { get; set; }
        public SortOrderEnum Order { get; set; }
    }
}