using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services.Interfaces.Base
{
    public interface IService<T> : IPartialService<T> where T : Entity
    {
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
