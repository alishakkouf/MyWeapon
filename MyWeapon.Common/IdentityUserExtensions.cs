using System.Security.Claims;
using MyWeapon.Shared;

namespace MyWeapon.Common
{
    public static class IdentityUserExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var id = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(id) ? (int?)null : int.Parse(id);
        }

        public static string? GetUserName(this ClaimsPrincipal user)
            => user?.FindFirst(ClaimTypes.Name)?.Value;

        public static int? GetTenantId(this ClaimsPrincipal user)
        {
            var tenantId = user?.FindFirst(Constants.TenantIdClaimType)?.Value;
            return string.IsNullOrEmpty(tenantId) ? (int?)null : int.Parse(tenantId);
        }

        public static List<string> GetPermissions(this ClaimsPrincipal user)
        {
            return user?.Claims.Where(c => c.Type == Constants.PermissionsClaimType).Select(c => c.Value).ToList() ?? [];
        }

        public static bool IsWithoutRole(this ClaimsPrincipal user)
        {
            var role = user?.FindFirst(ClaimTypes.Role)?.Value;
            return string.IsNullOrEmpty(role);
        }

        //public static string GetAccessToken(this HttpRequest request)
        //{
        //    return request.Headers.Authorization[0]["bearer ".Length..];
        //}
    }
}
