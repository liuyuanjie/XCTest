using Xcelerator.Data.Entity;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Repositories.Interfaces
{
    public interface IAuditRepository : IRepository<Audit, int>
    {
    }
}
