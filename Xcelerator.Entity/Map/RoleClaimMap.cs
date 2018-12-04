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
    public class RoleClaimMap : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("RoleClaim");

            builder.HasIndex(e => e.RoleId)
                .HasName("IX_RoleClaims_RoleId");
            builder.Property(e => e.ClaimType).HasMaxLength(256);
            builder.Property(e => e.ClaimValue).HasMaxLength(256);
            builder.HasOne(d => d.Role)
                .WithMany(p => p.Claims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleClaim_Role_RoleId_Id");

        }
    }
}
