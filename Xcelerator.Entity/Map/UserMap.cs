using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Xcelerator.Entity.Map
{
    public class UserMap
    {
        public static void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            // Need to use the old table name to map the entity Name.
            entityTypeBuilder.ToTable("User");

            // Need to use the old field name to map the Id property.
            //entityTypeBuilder.Property(p => p.Id).HasColumnName("UserId");

            //entityTypeBuilder
            //    .HasMany(u => u.Claims)
            //    .WithOne()
            //    .HasForeignKey(c => c.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            //entityTypeBuilder
            //    .HasMany(u => u.Roles)
            //    .WithOne()
            //    .HasForeignKey(r => r.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //entityTypeBuilder
            //    .HasMany(r => r.AuditUsers)
            //    .WithOne()
            //    .HasForeignKey(c => c.AuditId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //entityTypeBuilder
            //    .HasOne(r => r.Organization)
            //    .WithMany()
            //    .HasForeignKey(c => c.OrganizationId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
