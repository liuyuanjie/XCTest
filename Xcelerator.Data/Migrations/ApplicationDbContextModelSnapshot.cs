﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xcelerator.Data;

namespace Xcelerator.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Xcelerator.Data.Entity.AuditQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssignedUserId");

                    b.Property<int>("AuditId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Result")
                        .HasMaxLength(256);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AuditId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditQuestion");
                });

            modelBuilder.Entity("Xcelerator.Data.Entity.AuditUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AuditId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditUser");
                });

            modelBuilder.Entity("Xcelerator.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Xcelerator.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256);

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(256);

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(256);

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("LastName")
                        .HasMaxLength(256);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int>("OrganizationId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Xcelerator.Entity.UserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Xcelerator.Entity.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("Xcelerator.Entity.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Xcelerator.Entity.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<string>("LastModifiedBy");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Xcelerator.Entity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Xcelerator.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Xcelerator.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Xcelerator.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xcelerator.Data.Entity.AuditQuestion", b =>
                {
                    b.HasOne("Xcelerator.Entity.Audit", "Audit")
                        .WithMany("AuditQuestions")
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xcelerator.Entity.User", "User")
                        .WithMany("AuditQuestions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Xcelerator.Data.Entity.AuditUser", b =>
                {
                    b.HasOne("Xcelerator.Entity.Audit", "Audit")
                        .WithMany("AuditUsers")
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xcelerator.Entity.User", "User")
                        .WithMany("AuditUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xcelerator.Entity.User", b =>
                {
                    b.HasOne("Xcelerator.Entity.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xcelerator.Entity.UserRole", b =>
                {
                    b.HasOne("Xcelerator.Entity.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Xcelerator.Entity.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Xcelerator.Entity.Audit", b =>
                {
                    b.HasOne("Xcelerator.Entity.Template", "Template")
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
