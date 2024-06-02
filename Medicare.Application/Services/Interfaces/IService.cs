using Medicare.Domain.Entities.Base;

namespace Medicare.Application.Services.Interfaces
{
    public interface IService<T> : IPartialService<T> where T : Entity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<int> GetRowsCountAsync(CancellationToken cancellationToken);
        Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
