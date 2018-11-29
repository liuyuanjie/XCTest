using System.Threading.Tasks;

namespace Xcelerator.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}