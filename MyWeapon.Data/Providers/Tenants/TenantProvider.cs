using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWeapon.Data.Models;
using MyWeapon.Data.Models.Tenants;
using MyWeapon.Domain.Tenants;
using MyWeapon.Shared;
using MyWeapon.Shared.Exceptions;
using MyWeapon.Shared.RequestDtos;

namespace MyWeapon.Data.Providers.Tenants
{
    internal class TenantProvider : GenericProvider<Tenant>, ITenantProvider
    {
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserManager<UserAccount> _userManager;
        private readonly IMapper _mapper;

        public TenantProvider(MyWeaponDbContext dbContext,
             RoleManager<UserRole> roleManager,
             UserManager<UserAccount> userManager,
             IMapper mapper)
        {
            DbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<TenantDomain>> GetAllAsync(TenantListQuery request)
        {
            var query = CreateFilteredQuery(request);

            query = query.OrderBy(x => x.Name);

            return _mapper.Map<List<TenantDomain>>(await query.ToListAsync());
        }

        public async Task<TenantDomain> GetAsync(int id)
        {
            var tenant = await ActiveDbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (tenant == null)
                throw new EntityNotFoundException(nameof(Tenant), id.ToString());

            return _mapper.Map<TenantDomain>(tenant);
        }

        public async Task<TenantDomain> GetWithoutTenantAsync(int id)
        {
            var tenant = await DbContext.Tenants.FirstOrDefaultAsync(x => x.Id == id);

            if (tenant == null)
                throw new EntityNotFoundException(nameof(Tenant), id.ToString());

            return _mapper.Map<TenantDomain>(tenant);
        }

        public async Task<bool> DomainNameAlreadyExistsAsync(string name)
        {
            return await ActiveDbSet.AnyAsync(x => x.DomainName == name);
        }

        public async Task<TenantDomain> CreateAndSeedAsync(CreateTenantCommand command)
        {
            var tenant = new Tenant
            {
                Name = command.Name,
                DomainName = command.DomainName,
                AdminEmail = $"admin@{command.DomainName}",
                Country = command.Country,
                City = command.City,
                EncryptedId = string.Empty,
                Token = string.Empty,                
                IsActive = true
            };

            await DbContext.Tenants.AddAsync(tenant);
            await DbContext.SaveChangesAsync();

            await MyWeaponSeed.SeedStaticRolesAsync(_roleManager, tenant);
            await MyWeaponSeed.SeedDefaultUserAsync(_userManager, _roleManager, tenant, command.AdminPassword);
            await MyWeaponSeed.SeedDefaultSettingsAsync(DbContext, tenant, command);

            await DbContext.SaveChangesAsync();

            return _mapper.Map<TenantDomain>(tenant);
        }

        public Task<List<string>> GetActiveCitiesAsync()
        {
            return ActiveDbSet.Where(x => x.IsActive && !string.IsNullOrEmpty(x.City)).AsNoTracking()
                .Select(x => x.City)
                .Distinct()
                .ToListAsync();
        }

        public async Task UpdateSpecialtyAsync(int id, string specialty)
        {
            var tenant = await ActiveDbSet.FirstOrDefaultAsync(x => x.Id == id) ??
                throw new EntityNotFoundException(nameof(Tenant), id.ToString());
            await DbContext.SaveChangesAsync();
        }

        private IQueryable<Tenant> CreateFilteredQuery(TenantListQuery request)
        {
            var query = ActiveDbSet.AsNoTracking();

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.Name.Contains(request.Keyword) ||
                                         x.AdminEmail.Contains(request.Keyword) ||
                                         x.DomainName.Contains(request.Keyword));

            if (request.IsActive.HasValue)
                query = query.Where(x => x.IsActive == request.IsActive);

            return query;
        }

        private static IQueryable<Tenant> ApplyPaging(IQueryable<Tenant> query, PagedAndSortedResultRequestDto request)
        {
            var take = request.PageSize ?? Constants.DefaultPageSize;
            var skip = ((request.PageIndex ?? Constants.DefaultPageIndex) - 1) * take;

            return query.Skip(skip).Take(take);
        }

        public Task<List<TenantDomain>> GetAllAsync(bool? isActive)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDomain> GetCurrentTenantAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsActiveTenantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDomain> CreateTenantAsync(CreateTenantCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
