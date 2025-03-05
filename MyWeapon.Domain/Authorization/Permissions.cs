using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWeapon.Shared;

namespace MyWeapon.Domain.Authorization
{
    /// <summary>
    /// Static permissions of the system are defined here, and they will be added as claims (of type <see cref="Constants.PermissionsClaimType"/>)
    /// to the corresponding roles. Static role permissions are defined in <see cref="StaticRolePermissions"/>.
    /// </summary>
    public static class Permissions
    {
        private const string PermissionsPrefix = Constants.PermissionsClaimType + ".";

        public static readonly string[] ListAll =
        {

            Tenant.Create,
            Tenant.Delete,
            Tenant.Update,
            Tenant.View
        };


        #region Tenant Permissions
        public static class Tenant
        {
            public const string View = PermissionsPrefix + "Tenant.View";
            public const string Create = PermissionsPrefix + "Tenant.Create";
            public const string Update = PermissionsPrefix + "Tenant.Update";
            public const string Delete = PermissionsPrefix + "Tenant.Delete";
        }
        #endregion


    }
}
