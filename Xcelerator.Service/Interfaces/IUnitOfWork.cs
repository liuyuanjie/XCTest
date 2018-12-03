using System.Threading.Tasks;

namespace Xcelerator.Service.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        int SaveChanges();
    }
}