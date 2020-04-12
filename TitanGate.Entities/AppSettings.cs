using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities
{
    public class AppSettings
    {
        public string SqlConnectionString { get; set; }
        public string BaseAppFolder { get; set; }
        public string BaseFilesFolder { get; set; }
    }
}
