using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyWeapon.Data.Models.Tenants
{
    internal class Tenant : IAuditedEntity
    {
        public int Id { get; set; }

        [StringLength(200)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public string DomainName { get; set; }

        [StringLength(500)]
        public string AdminEmail { get; set; }

        [StringLength(500)]
        public string Country { get; set; }

        [StringLength(500)]
        public string City { get; set; }

        public bool IsActive { get; set; } = true;

        public long? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;

        [StringLength(50)]
        public string EncryptedId { get; set; }

        [StringLength(50)]
        public string Token { get; set; }
    }
}
