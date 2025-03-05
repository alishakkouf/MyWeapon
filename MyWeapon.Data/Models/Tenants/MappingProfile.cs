using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyWeapon.Domain.Tenants;

namespace MyWeapon.Data.Models.Tenants
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tenant, TenantDomain>();
        }
    }
}
