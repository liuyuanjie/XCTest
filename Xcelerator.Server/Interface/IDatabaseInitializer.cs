using System.Threading.Tasks;

namespace Xcelerator.Server.Interface
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
