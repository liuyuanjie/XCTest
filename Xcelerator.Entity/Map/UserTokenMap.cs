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
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("UserToken");

            builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            builder.Property(e => e.LoginProvider).HasMaxLength(256);
            builder.Property(e => e.Name).HasMaxLength(256);
            builder.HasOne(d => d.User)
                .WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserToken_User_UserId_Id");
        }
    }
}
