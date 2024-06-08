
using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Repositories.Base
{
    public interface IPartialRepository<T>: IReadOnlyRepository<T> where T : Entity
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
