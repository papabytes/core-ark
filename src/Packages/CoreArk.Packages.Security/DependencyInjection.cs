using CoreArk.Packages.Security.Configurations;
using CoreArk.Packages.Security.Services.SecurityTokens;
using CoreArk.Packages.Security.Services.SecurityTokens.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreArk.Packages.Security
{
    public static class DependencyInjection
    {
        public static void AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOptions>(configuration.GetSection("Security"));

            services.AddScoped<IJwtService, JwtService>();
        }
    }
}