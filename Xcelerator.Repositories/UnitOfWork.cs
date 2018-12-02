using System.Threading.Tasks;
using Xcelerator.Data;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
