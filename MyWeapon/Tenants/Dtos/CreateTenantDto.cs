using System.ComponentModel.DataAnnotations;
using MyWeapon.Shared;

namespace MyWeapon.Tenants.Dtos
{
    public class CreateTenantDto
    {      
        public required string Name { get; set; }

        [RegularExpression(Constants.DomainNameRegularExpression, ErrorMessage = "InvalidDomainFormat")]
        public required string DomainName { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "StringLength{2}")]
        public required string AdminPassword { get; set; }

        [RegularExpression(Constants.EmailRegularExpression, ErrorMessage = "InvalidEmailFormat")]
        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; } 

        public string PhoneCode { get; set; } 

        public string Phone { get; set; }

        public string Owner { get; set; }

        public void Normalize()
        {
            DomainName = DomainName.ToLower();
        }
    }
}
