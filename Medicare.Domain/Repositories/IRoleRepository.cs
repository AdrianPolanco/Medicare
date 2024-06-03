using Medicare.Domain.Entities;
using Medicare.Domain.Repositories.Base;

namespace Medicare.Domain.Repositories
{
    public interface IRoleRepository: IReadOnlyRepository<Role>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
