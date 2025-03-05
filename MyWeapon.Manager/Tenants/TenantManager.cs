using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using MyWeapon.Domain.Tenants;
using MyWeapon.Shared;
using MyWeapon.Shared.Exceptions;

namespace MyWeapon.Manager.Tenants
{
    public class TenantManager : ITenantManager
    {
        private readonly ITenantProvider _provider;
        private readonly IStringLocalizer _localizer;

        public TenantManager(ITenantProvider provider, IStringLocalizerFactory factory)
        {
            _provider = provider;
            _localizer = factory.Create(typeof(CommonResource));
        }

        public async Task<TenantDomain> CreateTenantAsync(CreateTenantCommand command)
        {
            //if (await _provider.DomainNameAlreadyExistsAsync(command.DomainName))
            //    throw new BusinessException(_localizer["DomainNameAlreadyExists{0}", command.DomainName]);

            var tenant = await _provider.CreateAndSeedAsync(command);

            return tenant;
        }

        public Task<List<TenantDomain>> GetAllAsync(bool? isActive)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDomain> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantDomain> GetCurrentTenantAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TenantDomain> GetWithoutTenantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsActiveTenantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCityAsync(int id, string cityName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNameAsync(int id, string clinicName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSpecialtyAsync(int TenantId, string specialty)
        {
            throw new NotImplementedException();
        }
    }
}
