using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MedCenter
{
    public class SensitiveJsonProvider : JsonConfigurationProvider
    {
        private readonly ISensitiveDataHandler _handler;
        
        public SensitiveJsonProvider(SensitiveJson source, ISensitiveDataHandler handler) 
            : base(source)
        {
            _handler = handler;
        }

        public override void Load(Stream stream)
        {
            base.Load(stream);

            // actual decryption of sensitive data happens here
            //Data["abc:password"] = _handler.Decrypt(Data["abc:password"]);
        }
    }
}


