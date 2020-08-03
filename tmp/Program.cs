using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.DataProtection;

namespace MedCenter.TMp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .ConfigureAppConfiguration((c, wb) => {
                //     AddConfiguration(c, wb);
                // })
                .ConfigureWebHostDefaults(wb =>
                {
                    wb.UseStartup<Startup>();
                });

        // private static void AddConfiguration(HostBuilderContext context, 
        //                                      IConfigurationBuilder builder)
        // {
        //     IConfiguration configuration = builder.Build();
        //     builder.IncludeSensitiveFile(new CryptoBridge("sensitive-conf"), "sensitive.json", false, true);
        // }
    }
}
