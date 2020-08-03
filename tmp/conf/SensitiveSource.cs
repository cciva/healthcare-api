using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace MedCenter
{
    public class SensitiveJson : JsonConfigurationSource
    {
        private readonly ISensitiveDataHandler _handler = null;

        public SensitiveJson(ISensitiveDataHandler handler)
        {
            _handler = handler;
        }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new SensitiveJsonProvider(this, _handler);
        }
    }
}


