

using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Repositories.Base
{
    public interface ISelectableRepository<T> : ISelectablePartialRepository<T>, IRepository<T> where T : Entity
    {
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        T DetachEntity(T entity, CancellationToken cancellationToken);
    }
}
