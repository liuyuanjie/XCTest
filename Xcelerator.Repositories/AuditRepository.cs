using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Repositories
{
    public class AuditRepository : Repository<Audit, int>, IAuditRepository
    {
        public AuditRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
