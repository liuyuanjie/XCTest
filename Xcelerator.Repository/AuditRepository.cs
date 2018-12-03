using Xcelerator.Data;
using Xcelerator.Data.Entity;
using Xcelerator.Entity;
using Xcelerator.Repositories.Interfaces;

namespace Xcelerator.Repositories
{
    public class OrganizationRepository : Repository<Organization, int>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
    }
}
