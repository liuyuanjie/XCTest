using System;
using System.ComponentModel.DataAnnotations;

namespace Xcelerator.Entity
{
    public interface ILoggerEntity
    {
        [MaxLength(256)]
        string CreatedBy { get; set; }
        [MaxLength(256)]
        string LastModifiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime LastModifiedDate { get; set; }
    }
}