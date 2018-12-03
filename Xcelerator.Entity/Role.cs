using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the User class
    public class Role : IdentityRole<int>, ILoggerEntity
    {
        public Role()
        {

        }

        public Role(string roleName) : base(roleName)
        {

        }

        public Role(string roleName, string description) : base(roleName)
        {
            Description = description;
        }

        public string Description { get; set; }
        [MaxLength(50)]
        public string CreatedBy { get; set; }
        [MaxLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}
