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
    public class AuditUserMap : IEntityTypeConfiguration<AuditUser>
    {
        public void Configure(EntityTypeBuilder<AuditUser> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("AuditUser");

            builder.HasIndex(e => new { e.AuditId, e.UserId })
                .HasName("UX_AuditUser_AuditId_UserId")
                .IsUnique();

            builder.HasOne(d => d.Audit)
                .WithMany(p => p.AuditUsers)
                .HasForeignKey(d => d.AuditId)
                .HasConstraintName("FK_AuditUser_Audit_AuditId_Id");

            builder.HasOne(d => d.User)
                .WithMany(p => p.AuditUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditUser_User_UserId_Id");
        }
    }
}
