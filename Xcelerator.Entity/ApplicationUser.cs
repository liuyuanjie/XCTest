using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>, ILoggerEntity
    {
        [StringLength(256)]
        public string JobTitle { get; set; }
        [StringLength(256)]
        public string FirstName { get; set; }
        [StringLength(256)]
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }
        [StringLength(256)]
        public string CreatedBy { get; set; }
        [StringLength(256)]
        public string LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int OrganizationId { get; set; }

        //public virtual ICollection<ApplicationUserRole> Roles { get; set; }
        //public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<AuditUser> AuditUsers { get; set; }
        public virtual ICollection<AuditQuestion> AuditQuestions { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
