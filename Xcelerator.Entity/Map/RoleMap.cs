using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Xcelerator.Entity.Map
{
    public class RoleMap
    {
        public static void Configure(EntityTypeBuilder<ApplicationRole> entityTypeBuilder)
        {
            // Need to use the old table name to map the entity Name.
            entityTypeBuilder.ToTable("Role");

            // Need to use the old field name to map the Id property.
            //entityTypeBuilder.Property(p => p.Id).HasColumnName("RoleId");

            //entityTypeBuilder
            //    .HasMany(r => r.Claims)
            //    .WithOne()
            //    .HasForeignKey(c => c.RoleId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            //entityTypeBuilder
            //    .HasMany(r => r.Users)
            //    .WithOne()
            //    .HasForeignKey(r => r.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
