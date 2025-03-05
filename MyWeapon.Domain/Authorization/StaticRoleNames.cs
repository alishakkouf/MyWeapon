using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Domain.Authorization
{
    public static class StaticRoleNames
    {
        public const string Administrator = nameof(Administrator);
        public const string Accountant = nameof(Accountant);
        public const string HR = nameof(HR);
        public const string Client = nameof(Client);
    }
}
