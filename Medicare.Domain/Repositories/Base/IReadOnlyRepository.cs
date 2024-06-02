using Medicare.Domain.Entities.Base;
using System.Linq.Expressions;


namespace Medicare.Domain.Repositories.Base
{
    public interface IReadOnlyRepository<T> where T : Entity
    {
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes);
        Task<int> GetRowsCountAsync(CancellationToken cancellationToken);
        Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
    }
}
