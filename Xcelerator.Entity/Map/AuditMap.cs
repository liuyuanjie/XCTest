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
    public class AuditMap
    {
        public static void Configure(EntityTypeBuilder<Audit> entityTypeBuilder)
        {
            // Need to use the old table name to map the entity Name.
            entityTypeBuilder.ToTable("Audit");

            //entityTypeBuilder
            //    .HasMany(r => r.AuditUsers)
            //    .WithOne()
            //    .HasForeignKey(c => c.UserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            //entityTypeBuilder
            //    .HasOne(r => r.Template)
            //    .WithOne()
            //    .HasForeignKey<Audit>(c => c.TemplateId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //entityTypeBuilder
            //    .HasMany(r => r.AuditQuestions)
            //    .WithOne()
            //    .HasForeignKey(c => c.AuditId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
