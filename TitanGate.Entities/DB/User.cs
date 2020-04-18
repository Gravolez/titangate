using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities.DB
{
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
