using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Entities.Exceptions
{
    public class WebSiteStoreException : ApplicationException
    {
        public WebSiteStoreException() { }

        public WebSiteStoreException(string message) : base(message) { }

        public WebSiteStoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}
