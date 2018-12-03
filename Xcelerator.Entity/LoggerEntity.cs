using System;
using System.ComponentModel.DataAnnotations;

namespace Xcelerator.Entity
{
    public class LoggerEntity : ILoggerEntity
    {
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        [MaxLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
