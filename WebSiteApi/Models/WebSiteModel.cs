using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class WebSiteModel
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public int Id { get; set; }
        public WebSiteCategoryEnum Category { get; set; }
        public object Email { get; internal set; }
        public object Password { get; internal set; }
    }
}
