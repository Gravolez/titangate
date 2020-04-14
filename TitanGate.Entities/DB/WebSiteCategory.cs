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
    }
}
