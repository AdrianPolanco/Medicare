using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Base
{
    public class ReadOnlyService<T> : IReadOnlyService<T> where T : Entity
    {
        protected readonly IReadOnlyRepository<T> _readOnlyRepository;
        public ReadOnlyService(IReadOnlyRepository<T> readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            return await _readOnlyRepository.GetByIdAsync(id, cancellationToken, includes);
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await _readOnlyRepository.GetByPagesAsync(page, cancellationToken, filter, includes);
        }

        public async Task<int> GetRowsCountAsync(CancellationToken cancellationToken)
        {
            return await _readOnlyRepository.GetRowsCountAsync(cancellationToken);
        }
    }
}
