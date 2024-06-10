

using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Base
{
    public class SelectableService<T> : Service<T>, ISelectableService<T> where T : Entity
    {
        private readonly ISelectableRepository<T> _repository;
        public SelectableService(ISelectableRepository<T> repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.GetAllAsync(cancellationToken, filter, includes);
        }
    }
}
