using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWeapon.Domain.Tenants;
using MyWeapon.Manager.Tenants;

namespace MyWeapon.Manager
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureManagerModule(this IServiceCollection services,
                                                                     IConfiguration configuration)
        {
            services.AddScoped<ITenantManager, TenantManager>();

            return services;
        }
    }
}
