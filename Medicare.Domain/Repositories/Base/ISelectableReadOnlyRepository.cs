using Medicare.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Medicare.Domain.Repositories.Base
{
    public interface ISelectableReadOnlyRepository<T>: IReadOnlyRepository<T> where T : Entity
    {
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
    }
}
