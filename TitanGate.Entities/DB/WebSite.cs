using System;

namespace TitanGate.WebSiteStore.Entities.DB
{
    public class WebSite
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public WebSiteCategory Category { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasScreenshot { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
