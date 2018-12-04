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
    public class UserLoginMap : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            // Need to use the old table name to map the entity Name.
            builder.ToTable("UserLogin");

            builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            builder.HasIndex(e => e.UserId)
                .HasName("IX_AspNetUserLogin_UserId");
            builder.Property(e => e.LoginProvider).HasMaxLength(256);
            builder.Property(e => e.ProviderKey).HasMaxLength(256);
            builder.HasOne(d => d.User)
                .WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogin_User_UserId_Id");
        }
    }
}
