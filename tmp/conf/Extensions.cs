using System;
using Microsoft.Extensions.Configuration;

namespace MedCenter
{
    public static class ConfExtensions
    {
        public static IConfigurationBuilder IncludeSensitiveFile(this IConfigurationBuilder builder,
                                                                ISensitiveDataHandler handler,
                                                                string path, 
                                                                bool optional,
                                                                bool reloadOnChange)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("File path must be a non-empty string.");

            var source = new SensitiveJson(handler)
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }
}


