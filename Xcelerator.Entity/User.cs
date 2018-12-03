using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser<int>, ILoggerEntity
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string JobTitle { get; set; }
        public bool IsEnabled { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        [StringLength(50)]
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? OrganizationId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<AuditUser> AuditUsers { get; set; }
        public virtual ICollection<AuditQuestion> AuditQuestions { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
    }
}
