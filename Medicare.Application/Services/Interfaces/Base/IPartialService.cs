using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services.Interfaces.Base
{
    public interface IPartialService<T> : IReadOnlyService<T> where T : Entity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
		Task UpdateAsync(T entity, CancellationToken cancellationToken);
	}
}
