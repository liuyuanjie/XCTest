using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    public class Audit : LoggerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public int TemplateId { get; set; }
        [Required]
        public byte Status { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        public int? OrganizationId { get; set; }

        [MaxLength(1024)]
        public string Result { get; set; }

        public virtual ICollection<AuditUser> AuditUsers { get; set; }
        public virtual ICollection<AuditQuestion> AuditQuestions { get; set; }

        public virtual Template Template { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
