using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}
