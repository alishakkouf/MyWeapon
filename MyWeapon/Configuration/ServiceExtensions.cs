using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyWeapon.Common.Filters;
using MyWeapon.Common;
using MyWeapon.Common.Middlewares;
using MyWeapon.Configuration.Authorization;
using MyWeapon.Data;
using MyWeapon.Data.Models;
using MyWeapon.Shared;
using MyWeapon.Domain.Settings;

namespace MyWeapon.Configuration
{
    internal static class ServiceExtensions
    {
        /// <summary>
        /// Add Controllers, AutoMapper, Cors
        /// </summary>
        internal static IServiceCollection ConfigureApiControllers(this IServiceCollection services, IConfiguration configuration, string corsPolicyName)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(new ValidationFilterAttribute());
                config.Filters.Add(new NormalizeFilterAttribute());
            })
                .AddDataAnnotationsLocalization(o =>
                {
                    o.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(CommonResource));
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = SupportedLanguages.ListAll.Select(x => new CultureInfo(x)).ToList();

                options.DefaultRequestCulture = new RequestCulture(SupportedLanguages.English);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.FallBackToParentUICultures = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddScoped<TemporalTenant>();

            return services;
        }


        /// <summary>
        /// Add Identity specific services, and Api Bearer Authentication
        /// </summary>
        internal static IServiceCollection ConfigureApiIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //configure password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            });

            services.AddIdentityCore<UserAccount>()
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<MyWeaponDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = configuration["IdentityServer:Authority"];
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                    };
                });

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, FailedAuthorizationWrapperHandler>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, FailedAuthorizationWrapperHandler>();

            // Default authorization policy = User must have the claim UserIsActive which is fetched from db
            // on PermissionsMiddleware
            services.AddAuthorization(options => options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireClaim(Constants.ActiveUserClaimType)
                .Build());

            services.AddScoped<IRolePermissionsService, RolePermissionsService>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }


        /// <summary>
        /// Adds the <see cref="PermissionsMiddleware"/> to the specified <see cref="IApplicationBuilder"/>,
        /// which retrieves permissions from the role of user and add them as claims.
        /// Must be called after UseAuthentication and before UseAuthorization.
        /// </summary>
        internal static IApplicationBuilder UseRolePermissions(this IApplicationBuilder app)
        {
            app.UseMiddleware<PermissionsMiddleware>();
            return app;
        }
    }
}
