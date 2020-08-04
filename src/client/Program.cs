using System;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var section = Configuration.GetSection("Sectionofsettings");
            //var section = Configuration.GetValue("Onesetting");
        }
    }
}
