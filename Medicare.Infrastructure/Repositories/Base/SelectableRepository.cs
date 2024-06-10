using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using System;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories.Base
{
    public class SelectableRepository<T> : Repository<T>, ISelectableRepository<T> where T : Entity
    {
        private readonly ISelectableReadOnlyRepository<T> _repository;
        public SelectableRepository(ApplicationDbContext context, ISelectableReadOnlyRepository<T> repository) : base(context)
        {
            _repository = repository;
        }

        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAllAsync(cancellationToken, filter, includes);
        }
    }
}
