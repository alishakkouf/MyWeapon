using Microsoft.AspNetCore.Identity;
using MyWeapon.Data.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyWeapon.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyWeapon.Data.Models.Settings;
using MyWeapon.Data.Models.Tenants;

namespace MyWeapon.Data
{
    public class MyWeaponDbContext : IdentityDbContext<UserAccount, UserRole, long>
    {
        private static readonly List<string> HardDeletedList = [nameof(UserRole)];

        internal DbSet<Tenant> Tenants { get; set; }
        internal DbSet<Setting> Settings { get; set; }

        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        private readonly int? _currentTenantId;
        private readonly bool _ignoreTenant;

        public MyWeaponDbContext(DbContextOptions<MyWeaponDbContext> options,
            IConfiguration configuration,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _configuration = configuration;
            _currentUserService = currentUserService;

            _currentTenantId = _currentUserService.GetTenantId();

            _ignoreTenant = _configuration[Constants.AppIgnoreTenantIdKey]?.ToLower() == "true";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //To remove exsistd index to handle repeted indexes within tenants
            builder.Entity<UserRole>(b =>
            {
                b.Metadata.RemoveIndex([b.Property(r => r.NormalizedName).Metadata]);
            });

            builder.Entity<UserRole>()
                .HasIndex(x => new { x.NormalizedName, x.TenantId }, "RoleNameIndex")
                .IsUnique();

            builder.Entity<UserAccount>()
                .HasMany(x => x.UserRoles)
                .WithMany(x => x.UserAccounts)
                .UsingEntity<IdentityUserRole<long>>(
                    r => r.HasOne<UserRole>().WithMany().HasForeignKey(x => x.RoleId),
                    l => l.HasOne<UserAccount>().WithMany().HasForeignKey(x => x.UserId));

            builder.Entity<UserRole>()
                .HasMany(x => x.Claims)
                .WithOne()
                .HasForeignKey(x => x.RoleId);


            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }

            if (!_ignoreTenant)
            {
                foreach (var type in builder.Model.GetEntityTypes())
                {
                    if (typeof(IHaveTenantId).IsAssignableFrom(type.ClrType))
                    {
                        var method = SetGlobalQueryMethod.MakeGenericMethod(type.ClrType);
                        method.Invoke(this, new object[] { builder });
                    }
                }

                //builder.Entity<ResetPassword>()
                //    .HasQueryFilter(x => x.UserAccount.TenantId == _currentTenantId);

            }
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(MyWeaponDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : class, IHaveTenantId
        {
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _currentTenantId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditedEntity auditedEntity)
                {
                    var userId = _currentUserService.GetUserId();

                    if (entry.State == EntityState.Added)
                    {
                        auditedEntity.CreatedAt = DateTime.UtcNow;
                        auditedEntity.CreatedBy = userId;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        auditedEntity.ModifiedAt = DateTime.UtcNow;
                        auditedEntity.ModifiedBy = userId;
                    }
                }

                if (_currentTenantId != null && entry.Entity is IHaveTenantId tenantEntity)
                {
                    // Set TenantId if not already set (only if you want to ensure TenantId for new entities)
                     FillTenantId(tenantEntity, entry.State, _currentTenantId);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void FillTenantId(IHaveTenantId entityWithTenant, EntityState entityState, int? tenantId)
        {
            switch (entityState)
            {
                case EntityState.Added:
                    entityWithTenant.TenantId = tenantId;
                    break;
            }
        }

        private void FillAuditedAndChangeDeleted(EntityEntry entry, long? userId)
        {
            if (entry.Entity is IAuditedEntity audited)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (!HardDeletedList.Contains(entry.Entity.GetType().Name))
                        {
                            entry.State = EntityState.Modified;
                            foreach (var entryProperty in entry.Properties)
                            {
                                if (entryProperty.Metadata.Name != nameof(audited.IsDeleted) &&
                                    entryProperty.Metadata.Name != nameof(audited.ModifiedAt) &&
                                    entryProperty.Metadata.Name != nameof(audited.ModifiedBy))
                                    entryProperty.IsModified = false;
                            }

                            audited.ModifiedAt = DateTime.UtcNow;
                            audited.ModifiedBy = userId;
                            audited.IsDeleted = true;
                        }
                        break;
                    case EntityState.Modified:
                        audited.ModifiedAt = DateTime.UtcNow;
                        audited.ModifiedBy = userId;
                        Entry(audited).Property(p => p.CreatedAt).IsModified = false;
                        Entry(audited).Property(p => p.CreatedBy).IsModified = false;
                        break;
                    case EntityState.Added:
                        audited.CreatedAt = DateTime.UtcNow;
                        audited.CreatedBy = userId;
                        audited.IsDeleted = false;
                        break;
                }
            }
        }

    }
}
