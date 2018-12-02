using System.Threading.Tasks;

namespace Xcelerator.Server.Interfaces
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
