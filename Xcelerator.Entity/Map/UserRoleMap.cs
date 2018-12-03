using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Xcelerator.Entity.Map
{
    //public class UserRoleMap
    //{
    //    public static void Configure(EntityTypeBuilder<UserRole> entityTypeBuilder)
    //    {
    //        // Need to use the old table name to map the entity Name.
    //        entityTypeBuilder.ToTable("UserRole");
    //    }
    //}

    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
        }
    }
}
