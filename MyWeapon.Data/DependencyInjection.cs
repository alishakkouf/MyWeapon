using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWeapon.Data.Models;
using MyWeapon.Data.Providers.Tenants;
using MyWeapon.Domain.Tenants;
using MyWeapon.Shared;

namespace MyWeapon.Data
{
    public static class DependencyInjection
    {
        const string ConnectionStringName = "DefaultConnection";
        const bool SeedData = true;

        public static IServiceCollection ConfigureDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyWeaponDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddProviders();

            return services;
        }

        public static async Task MigrateAndSeedDatabaseAsync(this IApplicationBuilder builder)
        {
            var scope = builder.ApplicationServices.CreateAsyncScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<MyWeaponDbContext>();

                if (context.Database.IsSqlServer())
                {
                    await context.Database.MigrateAsync();
                }

                if (SeedData)
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserAccount>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();
                    await MyWeaponSeed.SeedSuperAdminAsync(context, roleManager, userManager);
                    await MyWeaponSeed.SeedDefaultTenantAsync(context);

                    foreach (var tenant in await context.Tenants.Where(x => x.IsActive && x.IsDeleted != true).ToListAsync())
                    {
                        await MyWeaponSeed.SeedStaticRolesAsync(roleManager, tenant);
                        await MyWeaponSeed.SeedDefaultUserAsync(userManager, roleManager, tenant, Constants.DefaultPassword);
                        await MyWeaponSeed.SeedDefaultSettingsAsync(context, tenant);
                    }

                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<MyWeaponDbContext>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }


        private static void AddProviders(this IServiceCollection services)
        {            
            services.AddScoped<ITenantProvider, TenantProvider>();

            //services.AddTransient<IFireStoreProvider<IFirebaseEntity>, FireStoreProvider<IFirebaseEntity>>();
            //services.AddSingleton<FirestoreService>();


        }
    }
}
