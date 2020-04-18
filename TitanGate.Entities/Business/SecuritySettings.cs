using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities.Business
{
    public class SecuritySettings
    {
        public string JwtEncryptionKey { get; set; }
        public int TokenValidHours { get; set;  }
        public int RefreshTokenValidHours { get; set; }
    }
}
