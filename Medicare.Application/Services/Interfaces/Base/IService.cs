using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services.Interfaces.Base
{
    public interface IService<T> : IPartialService<T> where T : Entity
    {
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
