using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Entity.Map;

namespace Xcelerator.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
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

            UserMap.Configure(modelBuilder.Entity<ApplicationUser>());
            RoleMap.Configure(modelBuilder.Entity<ApplicationRole>());
            OrganizationMap.Configure(modelBuilder.Entity<Organization>());

            TemplateMap.Configure(modelBuilder.Entity<Template>());
            AuditMap.Configure(modelBuilder.Entity<Audit>());
            AuditQuestionMap.Configure(modelBuilder.Entity<AuditQuestion>());
            AuditUserMap.Configure(modelBuilder.Entity<AuditUser>());
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
