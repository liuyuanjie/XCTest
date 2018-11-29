using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Xcelerator.Data.Entity;

namespace Xcelerator.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
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

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(r => r.AuditUsers).WithOne().HasForeignKey(c => c.AuditId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ApplicationUser>().HasIndex(x => x.NormalizedEmail).IsUnique();

            modelBuilder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Audit>().Property(o => o.Name).HasMaxLength(500);
            modelBuilder.Entity<Audit>().ToTable("Audit");
            modelBuilder.Entity<Audit>().HasMany(r => r.AuditUsers).WithOne().HasForeignKey(c => c.AuditId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditUser>().HasOne(r => r.User).WithMany(x => x.AuditUsers).HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AuditUser>().HasOne(r => r.Audit).WithMany(x => x.AuditUsers).HasForeignKey(r => r.AuditId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuditUser>().ToTable("AuditUser");
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
