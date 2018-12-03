using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Entity.Map;

namespace Xcelerator.Data
{
    public class ApplicationDbContext :
        IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public string CurrentUserId { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<AuditQuestion> AuditQuestions { get; set; }
        public virtual DbSet<AuditUser> AuditUsers { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Template> Templates { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");

            modelBuilder.Entity<RoleClaim>().HasIndex(e => e.RoleId)
                .HasName("IX_RoleClaims_RoleId");
            modelBuilder.Entity<RoleClaim>().Property(e => e.ClaimType).HasMaxLength(256);
            modelBuilder.Entity<RoleClaim>().Property(e => e.ClaimValue).HasMaxLength(256);
            modelBuilder.Entity<RoleClaim>().HasOne(d => d.Role)
                .WithMany(p => p.Claims)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleClaim_Role_RoleId_Id");

            modelBuilder.Entity<UserClaim>().HasIndex(e => e.UserId)
                .HasName("IX_UserClaims_UserId");
            modelBuilder.Entity<UserClaim>().Property(e => e.ClaimType).HasMaxLength(256);
            modelBuilder.Entity<UserClaim>().Property(e => e.ClaimValue).HasMaxLength(256);
            modelBuilder.Entity<UserClaim>().HasOne(d => d.User)
                .WithMany(p => p.Claims)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserClaim_User_UserId_Id");

            modelBuilder.Entity<UserLogin>().HasKey(e => new { e.LoginProvider, e.ProviderKey });
            modelBuilder.Entity<UserLogin>().HasIndex(e => e.UserId)
                .HasName("IX_AspNetUserLogin_UserId");
            modelBuilder.Entity<UserLogin>().Property(e => e.LoginProvider).HasMaxLength(256);
            modelBuilder.Entity<UserLogin>().Property(e => e.ProviderKey).HasMaxLength(256);
            modelBuilder.Entity<UserLogin>().HasOne(d => d.User)
                .WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserLogin_User_UserId_Id");

            modelBuilder.Entity<UserToken>().HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            modelBuilder.Entity<UserToken>().Property(e => e.LoginProvider).HasMaxLength(256);
            modelBuilder.Entity<UserToken>().Property(e => e.Name).HasMaxLength(256);
            modelBuilder.Entity<UserToken>().HasOne(d => d.User)
                .WithMany(p => p.UserTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserToken_User_UserId_Id");

            modelBuilder.ApplyConfiguration(new OrganizationMap());
            modelBuilder.ApplyConfiguration(new TemplateMap());
            modelBuilder.ApplyConfiguration(new AuditMap());
            modelBuilder.ApplyConfiguration(new AuditQuestionMap());
            modelBuilder.ApplyConfiguration(new AuditUserMap());
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is ILoggerEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (ILoggerEntity)entry.Entity;
                DateTime now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.LastModifiedDate = now;
                entity.LastModifiedBy = CurrentUserId;
            }
        }
    }
}
