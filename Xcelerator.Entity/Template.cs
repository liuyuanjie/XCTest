using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    public class Template : LoggerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }
        public int? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
    }
}
