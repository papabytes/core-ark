using Microsoft.Extensions.DependencyInjection;

namespace CoreArk.Packages.Services
{
    public static class DependencyInjection
    {
        public static void AddContextService(this IServiceCollection services)
        {
            services.AddScoped<IContextService, ContextService>();
        }
    }
}