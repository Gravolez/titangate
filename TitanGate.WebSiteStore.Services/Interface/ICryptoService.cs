using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Services
{
    public interface ICryptoService
    {
        string Encrypt(string toEncrypt);
        string Decrypt(string toDecrypt);
        string HashPassword(string password, string salt);
        byte[] GetSalt();
    }
}
