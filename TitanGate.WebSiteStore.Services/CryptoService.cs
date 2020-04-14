using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.Services;

namespace TitanGate.WebSiteStore.Services
{
    public class CryptoService : ICryptoService
    {
        public string Decrypt(string toDecrypt)
        {
            return toDecrypt;
        }

        public string Encrypt(string toEncrypt)
        {
            return toEncrypt;
        }
    }
}
