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
        IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public string CurrentUserId { get; set; }
        public DbSet<Audit> Audits { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            //UserMap.Configure(modelBuilder.Entity<User>());
            //RoleMap.Configure(modelBuilder.Entity<Role>());
            //UserRoleMap.Configure(modelBuilder.Entity<UserRole>());

            //OrganizationMap.Configure(modelBuilder.Entity<Organization>());
            //TemplateMap.Configure(modelBuilder.Entity<Template>());
            //AuditMap.Configure(modelBuilder.Entity<Audit>());
            //AuditQuestionMap.Configure(modelBuilder.Entity<AuditQuestion>());
            //AuditUserMap.Configure(modelBuilder.Entity<AuditUser>());
            //modelBuilder.ApplyConfiguration<User>();
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
