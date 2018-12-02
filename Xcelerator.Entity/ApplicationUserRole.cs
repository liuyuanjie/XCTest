using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        //public virtual ApplicationRole Role { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }
}
