using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWeapon.Data.Models;
using MyWeapon.Data.Models.Settings;
using MyWeapon.Data.Models.Tenants;
using MyWeapon.Domain.Authorization;
using MyWeapon.Domain.Settings;
using MyWeapon.Domain.Tenants;
using MyWeapon.Shared;
using Serilog;

namespace MyWeapon.Data
{
    internal static class MyWeaponSeed
    {
        /// <summary>
        /// Seed default and first Tenant with id = 1
        /// </summary>
        public static async Task SeedDefaultTenantAsync(MyWeaponDbContext context)
        {
            if (!context.Tenants.Any(x => x.Id == Constants.DefaultTenantId))
            {
                await context.Database.OpenConnectionAsync();
                try
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants ON");

                    var Tenant = new Tenant
                    {
                        Id = Constants.DefaultTenantId,
                        Name = "Shakkouf",
                        AdminEmail = Constants.DefaultTenantAdmin,
                        DomainName = Constants.DefaultTenantDomain,
                        IsActive = true,
                        IsDeleted = false,
                        Country = "Syria",
                        City = "Safita",
                        EncryptedId = string.Empty,
                        Token = string.Empty
                    };

                    await context.Tenants.AddAsync(Tenant);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Tenants OFF");
                }
                finally
                {
                    await context.Database.CloseConnectionAsync();
                }
            }
        }

        /// <summary>
        /// Seed super admin user.
        /// </summary>
        internal static async Task SeedSuperAdminAsync(MyWeaponDbContext context, RoleManager<UserRole> roleManager, UserManager<UserAccount> userManager)
        {
            var role = await context.Roles.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name == Constants.SuperAdminRoleName);
            
            if (role == null)
            {
                role = new UserRole(Constants.SuperAdminRoleName) { IsActive = true,Description = string.Empty, TenantId = null };
                await roleManager.CreateAsync(role);
            }

            var user = await context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserName.StartsWith(Constants.SuperAdminUserName));
            
            if (user == null)
            {
                user = new UserAccount
                {
                    UserName = Constants.SuperAdminEmail,
                    Email = Constants.SuperAdminEmail,
                    FirstName = "Super Admin",
                    LastName = "Super Admin",
                    PhoneNumber = "935479586",
                    IsActive = true,
                    TenantId = null,
                    City = string.Empty,
                    Country = string.Empty,
                    Nationality = string.Empty,
                    Gender = Shared.Enums.Gender.Male,
                    ConfirmationCode = string.Empty,
                    ImageRelativePath = string.Empty,
                    PhoneCountryCode = "+963",

                };

                var result = await userManager.CreateAsync(user, Constants.DefaultPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Log.Error($"Error: {error.Code} - {error.Description}");
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                }
                await userManager.AddToRolesAsync(user, new[] { Constants.SuperAdminRoleName });
            }
        }

        /// <summary>
        /// Seed static roles and add permissions claims to them.
        /// </summary>
        internal static async Task SeedStaticRolesAsync(RoleManager<UserRole> roleManager, Tenant Tenant)
        {
            foreach (var rolePermission in StaticRolePermissions.Roles)
            {
                var role = await roleManager.Roles.IgnoreQueryFilters().FirstOrDefaultAsync(x =>
                    x.NormalizedName == rolePermission.Key.ToUpper() && x.TenantId == Tenant.Id);

                if (role == null)
                {
                    role = new UserRole(rolePermission.Key)
                    {
                        IsActive = true,
                        TenantId = Tenant.Id,
                        Description = string.Empty,
                    };

                    await roleManager.CreateAsync(role);

                    //Add static role permissions to db
                    foreach (var permission in rolePermission.Value)
                    {
                        await roleManager.AddClaimAsync(role,
                            new Claim(Constants.PermissionsClaimType, permission));
                    }

                    continue;
                }

                if (rolePermission.Key == StaticRoleNames.Administrator)
                {
                    var dbRoleClaims = await roleManager.GetClaimsAsync(role);

                    //Remove any claim in db and not in static role permissions.
                    foreach (var dbPermission in dbRoleClaims.Where(x => x.Type == Constants.PermissionsClaimType &&
                                                                         !rolePermission.Value.Contains(x.Value)))
                    {
                        await roleManager.RemoveClaimAsync(role, dbPermission);
                    }

                    //Add static role permissions to db if they don't already exist.
                    foreach (var permission in rolePermission.Value)
                    {
                        if (!dbRoleClaims.Any(x => x.Type == Constants.PermissionsClaimType && x.Value == permission))
                        {
                            await roleManager.AddClaimAsync(role,
                                new Claim(Constants.PermissionsClaimType, permission));
                        }
                    }
                }
            }
        }

        internal static async Task SeedDefaultUserAsync(UserManager<UserAccount> userManager,
            RoleManager<UserRole> roleManager, Tenant Tenant, string adminPassword)
        {
            var adminRole = await roleManager.Roles.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Name == StaticRoleNames.Administrator && x.TenantId == Tenant.Id);

            var adminUser = await userManager.Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.UserName == Tenant.AdminEmail && x.TenantId == Tenant.Id);

            if (adminUser == null && adminRole != null)
            {
                adminUser = new UserAccount
                {
                    UserName = Tenant.AdminEmail,
                    Email = Tenant.AdminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "0123456789",
                    IsActive = true,
                    TenantId = Tenant.Id,
                    City = string.Empty,
                    Country = string.Empty,
                    Nationality = string.Empty,
                    Gender = Shared.Enums.Gender.Male,
                    ConfirmationCode = string.Empty,
                    ImageRelativePath = string.Empty,
                    PhoneCountryCode = string.Empty
                };
                adminUser.UserRoles.Add(adminRole);

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Log.Error($"Error: {error.Code} - {error.Description}");
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                }
            }
        }

        internal static async Task SeedDefaultSettingsAsync(MyWeaponDbContext context, Tenant Tenant)
        {
            var save = false;
            foreach (var setting in SettingDefaults.Defaults)
            {
                if (!context.Settings.IgnoreQueryFilters()
                    .Any(x => x.Name == setting.Key && x.UserId == null && x.TenantId == Tenant.Id))
                {
                    await context.Settings.AddAsync(new Setting
                    {
                        Name = setting.Key,
                        Value = setting.Value,
                        UserId = null,
                        TenantId = Tenant.Id,
                        Description = string.Empty
                    });
                    save = true;
                }
            }

            if (save)
                await context.SaveChangesAsync();
        }

        public static async Task SeedDefaultSettingsAsync(MyWeaponDbContext context, Tenant Tenant, CreateTenantCommand command)
        {
            var save = false;
            foreach (var setting in SettingDefaults.Defaults)
            {
                var key = setting.Key;
                var value = setting.Value;

                switch (key)
                {
                    case SettingNames.TenantName:
                        value = command.Name;
                        break;
                    case SettingNames.TenantDomainName:
                        value = command.DomainName;
                        break;
                    case SettingNames.TenantEmail:
                        value = command.Email;
                        break;
                    case SettingNames.Country:
                        value = command.Country;
                        break;
                    case SettingNames.City:
                        value = command.City;
                        break;
                    case SettingNames.AppCountryPhoneCode:
                        value = command.PhoneCode;
                        break;
                    case SettingNames.TenantPhone:
                        value = command.Phone;
                        break;
                    case SettingNames.TenantOwner:
                        value = command.Owner;
                        break;
                    case SettingNames.AppTimeZone:
                        value = GetDefaultTimeZone(command.Country) ?? value;
                        break;

                }
                if (!context.Settings.IgnoreQueryFilters()
                        .Any(x => x.Name == key && x.UserId == null && x.TenantId == Tenant.Id))
                {
                    await context.Settings.AddAsync(new Setting
                    {
                        Name = key,
                        Value = value,
                        Description = string.Empty,
                        UserId = null,
                        TenantId = Tenant.Id
                    });

                    save = true;
                }
            }

            if (save)
                await context.SaveChangesAsync();
        }

        private static string GetDefaultTimeZone(string country)
        {
            switch (country)
            {
                case Constants.GermanyCountryName:
                    return "Europe/Berlin";
                case Constants.UAECountryName:
                    return "Asia/Dubai";
                case Constants.EgyptCountryName:
                    return "Africa/Cairo";
                case Constants.SyriaCountryName:
                    return "Asia/Damascus";
                case Constants.PalestineCountryName:
                    return "Asia/Gaza";
                case Constants.JordanCountryName:
                    return "Asia/Amman";
                case Constants.YemenCountryName:
                    return "Asia/Aden";
                case Constants.LibyaCountryName:
                    return "Africa/Tripoli";
                case Constants.IraqCountryName:
                    return "Asia/Baghdad";
                case Constants.TurkeyCountryName:
                    return "Europe/Istanbul";
            }

            return null;
        }
    }
}
