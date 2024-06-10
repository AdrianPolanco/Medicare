using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories.Base
{
    public class SelectablePartialRepository<T> : PartialRepository<T>, ISelectablePartialRepository<T>, IPartialRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ISelectableReadOnlyRepository<T> _repository;    
        public SelectablePartialRepository(ApplicationDbContext context, ISelectableReadOnlyRepository<T> repository) : base(context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAllAsync(cancellationToken, filter, includes);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null)
        {
            return await _repository.GetByPagesAsync(page, cancellationToken, filter);
        }

    }
}
