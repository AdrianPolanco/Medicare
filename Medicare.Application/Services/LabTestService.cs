
using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;

namespace Medicare.Application.Services
{
    public class LabTestService: SelectableService<LabTest>, ILabTestService
    {
        public LabTestService(ILabTestRepository labTestRepository): base(labTestRepository)
        {
            
        }
    }
}
