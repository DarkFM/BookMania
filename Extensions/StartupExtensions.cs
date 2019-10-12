using BookMania.Infrastructure.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookMania.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGoogleBooks(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleApiOptions>(configuration);
            return services;
        }
    }
}
