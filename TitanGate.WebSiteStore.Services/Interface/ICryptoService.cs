using System;
using System.Collections.Generic;
using System.Text;

namespace TitanGate.WebSiteStore.Services
{
    public interface ICryptoService
    {
        string Encrypt(string toEncrypt);
        string Decrypt(string toDecrypt);
    }
}
