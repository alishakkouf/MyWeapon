using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Domain.Tenants
{
    public class TenantListQuery
    {
        public string Keyword { get; set; }

        public bool? IsActive { get; set; }
    }
}
