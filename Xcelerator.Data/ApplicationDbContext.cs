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
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());

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
