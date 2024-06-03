

using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities;

namespace Medicare.Application.Services.Interfaces
{
    public interface IRoleService: IReadOnlyService<Role>
    {
        Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
