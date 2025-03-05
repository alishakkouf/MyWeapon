using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyWeapon.Data;
using MyWeapon.Shared;

namespace MyWeapon.Common
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor, TemporalTenant temporalTenant) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly TemporalTenant _temporalTenant = temporalTenant;
        
        public int? GetTenantId()
        {
            if (_temporalTenant.TenantId != null) return _temporalTenant.TenantId;

            var tenantClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(Constants.TenantIdClaimType);

            if (int.TryParse(tenantClaim, out var tenantId))
                return tenantId;

            return null;
        }

        public int? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserId();
        }

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserName();
        }

        public string? GetUserFullName()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName);
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
        }

        public bool HasPermission(string permission)
        {
            return _httpContextAccessor.HttpContext?.User.HasClaim(x => x.Type == Constants.PermissionsClaimType && x.Value == permission) ?? false;
        }

        public string? GetUserRole()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        }
    }
}
