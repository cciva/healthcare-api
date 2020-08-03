using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace MedCenter
{
    // this implementation is meant to be used as encryption
    // bridge for managing our secrets and sensitive data
    // WARNING : THIS IS JUST FOR ASSESMENT DEMONSTRATION
    // PURPOSES ONLY. DO NOT USE IN REAL-WORLD SCENARIOS
    public class CryptoBridge : ISensitiveDataHandler
    {
        private readonly string _id = string.Empty;
        private readonly IDataProtectionProvider _provider;

        public CryptoBridge(string id)
        {
            _provider = DataProtectionProvider.Create("medcenter-api");
            _id = id;
        }

        public string Encrypt(Stream plain)
        {
            string final = string.Empty;
            using(StreamReader reader = new StreamReader(plain))
            {
                final = this.Encrypt(reader.ReadToEnd());
            }
            return final;
        }

        public string Encrypt(string plain)
        {
            var protector = ProtectionProvider.CreateProtector(Id);
            return protector.Protect(plain);
        }

        public string Decrypt(Stream cipher)
        {
            string final = string.Empty;
            using(StreamReader reader = new StreamReader(cipher))
            {
                final = this.Decrypt(reader.ReadToEnd());
            }
            return final;
        }

        public string Decrypt(string cipher)
        {
            var protector = ProtectionProvider.CreateProtector(Id);
            return protector.Unprotect(cipher);
        }
        
        public IDataProtectionProvider ProtectionProvider
        {
            get 
            {
                return _provider;
            }
        }

        public string Id
        {
            get { return _id; }
        }
    }
}