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
    public class UserClaimMap : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("UserClaim");

            builder.HasIndex(e => e.UserId)
                .HasName("IX_UserClaims_UserId");
            builder.Property(e => e.ClaimType).HasMaxLength(256);
            builder.Property(e => e.ClaimValue).HasMaxLength(256);
            builder.HasOne(d => d.User)
                .WithMany(p => p.Claims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserClaim_User_UserId_Id");
        }
    }
}
