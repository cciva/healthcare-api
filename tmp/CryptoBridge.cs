using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace MedCenter.Tmp
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
        /*
        private static readonly PrivateKey = 
        public string Encrypt(Stream stream)
        {
            string final = string.Empty;
            using(StreamReader reader = new StreamReader(stream))
            {
                final = this.Encrypt(reader.ReadToEnd());
            }

            return final;
        }

        public string Encrypt(string str)
        {
            string key = "abc123";
            string final = string.Empty;
            byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
            byte[] clearBytes = Encoding.Unicode.GetBytes(str);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    final = Convert.ToBase64String(ms.ToArray());
                }
            }
            
            return final;
        }

        public string Decrypt(Stream stream)
        {
            string final = string.Empty;
            using(StreamReader reader = new StreamReader(stream))
            {
                final = this.Decrypt(reader.ReadToEnd());
            }

            return final;
        }

        public string Decrypt(string input)
        {
            string key = "abc123";
            string final = string.Empty;
            string cipher = input.Replace(" ", "+");
            byte[] salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
            byte[] cipherBytes = Convert.FromBase64String(cipher);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    final = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return final;
        }
        */
    }
}