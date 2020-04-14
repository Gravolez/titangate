using System;
using System.Collections.Generic;
using System.Text;
using TitanGate.WebSiteStore.Services;
using System.Security.Cryptography;
using TitanGate.WebSiteStore.Entities.Exceptions;
using System.IO;

namespace TitanGate.WebSiteStore.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly byte[] _key = new byte[] { 0xAD, 0x46, 0x91, 0x15, 0x2F, 0x0C, 0x47, 0x8A, 0x85, 0xF7, 0x2A, 0x13, 0x2E, 0xE8, 0xAB, 0xE6, 0x6F, 0x2C, 0xDD, 0xAE, 0x52, 0x91, 0x4C, 0x68 };
        private readonly byte[] _iv = new byte[] { 0x20, 0xF7, 0xC5, 0x6B, 0x82, 0x5E, 0xC7, 0x88, 0x35, 0x64, 0xE5, 0x95, 0xE5, 0x70, 0xBF, 0xF4 };

        public string Decrypt(string toDecrypt)
        {
            if (string.IsNullOrEmpty(toDecrypt))
            {
                throw new WebSiteStoreException("Argument toDecrypt cannot be null");
            }

            using Aes aesAlg = Aes.Create();
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            byte[] bytes = Convert.FromBase64String(toDecrypt);
            using MemoryStream msDecrypt = new MemoryStream(bytes);
            using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

        public string Encrypt(string toEncrypt)
        {
            if (string.IsNullOrEmpty(toEncrypt))
            {
                throw new WebSiteStoreException("Argument toEncrypt cannot be null");
            }

            using Aes aesAlg = Aes.Create();
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(_key, _iv);
            using MemoryStream msEncrypt = new MemoryStream();
            using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using StreamWriter swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(toEncrypt);
            swEncrypt.Flush();
            byte[] encrypted = msEncrypt.ToArray();
            return Convert.ToBase64String(encrypted);
        }
    }
}
