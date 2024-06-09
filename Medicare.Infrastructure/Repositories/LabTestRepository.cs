using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;

namespace Medicare.Infrastructure.Repositories
{
    public class LabTestRepository: Repository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {
            
        }
    }
}
