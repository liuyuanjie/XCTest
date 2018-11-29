using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Xcelerator.Data.Entity
{
    public class Audit : LoggerEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<AuditUser> AuditUsers { get; set; }
    }
}
