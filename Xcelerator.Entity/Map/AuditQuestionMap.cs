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
    public class AuditQuestionMap
    {
        public static void Configure(EntityTypeBuilder<AuditQuestion> entityTypeBuilder)
        {
            // Need to use the old table name to map the entity Name.
            entityTypeBuilder.ToTable("AuditQuestion");

            //entityTypeBuilder
            //    .HasOne(r => r.Audit)
            //    .WithMany(c => c.AuditQuestions)
            //    .HasForeignKey(c => c.AuditId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            //entityTypeBuilder
            //    .HasOne(r => r.User)
            //    .WithMany(c => c.AuditQuestions)
            //    .HasForeignKey(c => c.AssignedUserId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
