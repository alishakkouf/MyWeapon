using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Domain.Tenants
{
    public interface ITenantManager
    {
        /// <summary>
        /// Get all Tenants.
        /// Filtered by isActive.
        /// </summary>
        Task<List<TenantDomain>> GetAllAsync(bool? isActive);

        /// <summary>
        /// Get Tenant data.
        /// </summary>
        Task<TenantDomain> GetAsync(int id);

        /// <summary>
        ///  Get Tenant data for unauthorized request.
        /// </summary>
        Task<TenantDomain> GetWithoutTenantAsync(int id);

        /// <summary>
        /// Get current Tenant.
        /// Return nulls if no Tenant is provided.
        /// </summary>
        Task<TenantDomain> GetCurrentTenantAsync();

        /// <summary>
        /// Check if Tenant with id is active.
        /// </summary>
        Task<bool> IsActiveTenantAsync(int id);

        /// <summary>
        /// Create new clinic
        /// </summary>
        Task<TenantDomain> CreateTenantAsync(CreateTenantCommand command);
    }
}
