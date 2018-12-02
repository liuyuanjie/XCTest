using System.Threading.Tasks;

namespace Xcelerator.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}