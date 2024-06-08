using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Base
{
    public abstract class PartialService<T> : IPartialService<T> where T : Entity
    {
        protected readonly IPartialRepository<T> _partialRepository;
        protected PartialService(IPartialRepository<T> partialRepository) {
            _partialRepository = partialRepository;
        }
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                return await _partialRepository.AddAsync(entity, cancellationToken);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            return await _partialRepository.GetByIdAsync(id, cancellationToken, includes);
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return await _partialRepository.GetByPagesAsync(page, cancellationToken, filter, includes);
        }

        public async Task<int> GetRowsCountAsync(CancellationToken cancellationToken)
        {
            return await _partialRepository.GetRowsCountAsync(cancellationToken);
        }

		public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
		{
			await _partialRepository.UpdateAsync(entity, cancellationToken);
		}
	}
}
