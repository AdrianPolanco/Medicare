using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class OfficeService: PartialService<Office>, IOfficeService
    {
        public OfficeService(IOfficeRepository repository) : base(repository)
        {
        }

        public override async Task<ICollection<Office>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Office, bool>> filter, params Expression<Func<Office, object>>[] includes)
        {
            return await base.GetByPagesAsync(page, cancellationToken, null, office => office.Users, office => office.Owner);
        }

        public override async Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Office, object>>[] includes)
        {
            return await base.GetByIdAsync(id, cancellationToken, office => office.Users, office => office.Owner);
        }
    }
}
