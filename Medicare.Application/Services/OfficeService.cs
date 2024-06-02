using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;

namespace Medicare.Application.Services
{
    public class OfficeService: PartialService<Office>, IOfficeService
    {
        public OfficeService(IOfficeRepository repository) : base(repository)
        {
        }
    }
}
