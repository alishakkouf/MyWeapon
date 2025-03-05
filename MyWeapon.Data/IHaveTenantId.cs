using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Data
{
    public interface IHaveTenantId
    {
        public int? TenantId { get; set; }
    }
}
