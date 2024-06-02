using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services.Interfaces
{
    public interface IPartialService<T> where T : Entity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
    }
}
