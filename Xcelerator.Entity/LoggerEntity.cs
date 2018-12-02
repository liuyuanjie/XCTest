using System;
using System.ComponentModel.DataAnnotations;

namespace Xcelerator.Entity
{
    public class LoggerEntity : ILoggerEntity
    {
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
