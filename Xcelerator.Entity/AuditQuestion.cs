using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Entity;

namespace Xcelerator.Data.Entity
{
    public class AuditQuestion : LoggerEntity
    {
        [Key]
        public int Id { get; set; }
        public int AuditId { get; set; }
        public int QuestionId { get; set; }
        public int AssignedUserId { get; set; }
        [StringLength(256)]
        public string Result { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual Audit Audit { get; set; }
        public virtual User User { get; set; }

    }
}