using System.Threading.Tasks;

namespace Xcelerator.Service.Interfaces
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
