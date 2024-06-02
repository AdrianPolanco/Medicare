using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;

namespace Medicare.Infrastructure.Repositories
{
    public class OfficeRepository : PartialRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
