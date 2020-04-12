using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities.DB
{
    public class WebSiteCategory
    {
        public WebSiteCategory() { }

        public WebSiteCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public static WebSiteCategory Porn => new WebSiteCategory(1, "Porn");

        public static WebSiteCategory Betting => new WebSiteCategory(2, "Betting");

        public static WebSiteCategory Entertainment => new WebSiteCategory(3, "Entertainment");

        public static WebSiteCategory Science => new WebSiteCategory(4, "Science");
    }
}
