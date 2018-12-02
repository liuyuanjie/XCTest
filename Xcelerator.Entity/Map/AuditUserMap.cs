using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xcelerator.Data.Entity;

namespace Xcelerator.Entity.Map
{
    public class AuditUserMap
    {
        public static void Configure(EntityTypeBuilder<AuditUser> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("AuditUser");

            //entityTypeBuilder
            //    .HasOne(r => r.User)
            //    .WithMany(x => x.AuditUsers)
            //    .HasForeignKey(x => x.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //entityTypeBuilder
            //    .HasOne(r => r.Audit)
            //    .WithMany(x => x.AuditUsers)
            //    .HasForeignKey(r => r.AuditId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
