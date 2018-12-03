using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class UserRole : IdentityUserRole<int>
    {
        //public virtual Role Role { get; set; }
        //public virtual User User { get; set; }
    }
}
