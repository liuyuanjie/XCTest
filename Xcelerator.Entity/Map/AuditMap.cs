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
    public class AuditMap : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("Audit");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");
            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.HasOne(d => d.Organization)
                .WithMany(p => p.Audits)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_Audit_Organization_OrganizationId_Id");

            builder.HasOne(d => d.Template)
                .WithMany(p => p.Audits)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Audit_Template_TemplateId_Id");
        }
    }
}
