using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities.Exceptions
{
    public class WebSiteStoreNotFoundException : WebSiteStoreException
    {
        public WebSiteStoreNotFoundException() { }

        public WebSiteStoreNotFoundException(string message) : base(message) { }

        public WebSiteStoreNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
