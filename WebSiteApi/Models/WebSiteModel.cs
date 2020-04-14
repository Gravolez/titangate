using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class WebSiteModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public CategoryModel Category { get; set; }
        public string Email { get; internal set; }
        public string Password { get; set; }
        public string ScreenshotUrl { get; set; }
    }
}
