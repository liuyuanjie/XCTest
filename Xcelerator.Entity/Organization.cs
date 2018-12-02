using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    public class Organization : LoggerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
