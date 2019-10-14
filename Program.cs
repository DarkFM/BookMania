using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookMania.Infrastructure.Config;
using BookMania.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookMania
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var googleOptions = services.GetRequiredService<IOptionsMonitor<GoogleApiOptions>>();
                var dbContext = services.GetRequiredService<CatalogContext>();

                dbContext.Database.Migrate();

                var seeder = new CatalogContextSeed(new System.Net.Http.HttpClient(), googleOptions, dbContext);
                seeder.SeedDataAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => config.AddEnvironmentVariables(prefix: "Google_"))
            .UseStartup<Startup>();
    }
}
