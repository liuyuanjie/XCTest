using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Xcelerator.Entity.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");
            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.HasIndex(e => e.Email)
                .HasName("UX_User_Email")
                .IsUnique();

            builder.HasIndex(e => e.UserName)
                .HasName("UX_User_UserName")
                .IsUnique();

            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.NormalizedEmail).HasMaxLength(256);
            builder.Property(e => e.NormalizedUserName).HasMaxLength(256);
            builder.Property(e => e.PhoneNumber).HasMaxLength(256);
            builder.Property(e => e.UserName).HasMaxLength(256);

            builder.HasOne(d => d.Organization)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("FK_User_Organization_OrganizationId_Id");
        }
    }
}
