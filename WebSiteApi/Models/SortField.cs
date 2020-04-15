using System;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class SortField
    {
        public SortFieldModelEnum Field { get; set; }
        public SortOrderModelEnum Order { get; set; }
    }
}