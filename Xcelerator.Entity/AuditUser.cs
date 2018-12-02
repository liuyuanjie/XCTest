using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Entity;

namespace Xcelerator.Data.Entity
{
    public class AuditUser : LoggerEntity
    {
        [Key]
        public int Id { get; set; }
        public int AuditId { get; set; }
        public int UserId { get; set; }

        public virtual Audit Audit { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}