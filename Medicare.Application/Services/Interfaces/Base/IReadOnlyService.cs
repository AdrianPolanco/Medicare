using Medicare.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Interfaces.Base
{
    public interface IReadOnlyService<T> where T : Entity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken,
            params Expression<Func<T, object>>[] includes);
        Task<int> GetRowsCountAsync(CancellationToken cancellationToken);
        Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);
    }
}
