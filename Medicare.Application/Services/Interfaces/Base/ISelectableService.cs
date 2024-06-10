

using Medicare.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Interfaces.Base
{
    public interface ISelectableService<T>: IService<T> where T : Entity
    {
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes);
    }
}
