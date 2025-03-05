using AutoMapper;
using MyWeapon.Domain.Tenants;

namespace MyWeapon.Tenants.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTenantDto, CreateTenantCommand>();
            CreateMap<TenantDomain, TenantDto>();
        }
    }
}
