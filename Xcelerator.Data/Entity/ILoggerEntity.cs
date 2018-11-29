using System;
using System.ComponentModel.DataAnnotations;

namespace Xcelerator.Data.Entity
{
    public interface ILoggerEntity
    {
        [MaxLength(100)]
        string CreatedBy { get; set; }
        [MaxLength(100)]
        string LastModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastModifiedDate { get; set; }
    }
}