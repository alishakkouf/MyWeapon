using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Domain.Tenants
{
    public class CreateTenantCommand
    {
        public string Name { get; set; }

        public string DomainName { get; set; }

        public string AdminPassword { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PhoneCode { get; set; }

        public string Phone { get; set; }

        public string Owner { get; set; }

    }
}
