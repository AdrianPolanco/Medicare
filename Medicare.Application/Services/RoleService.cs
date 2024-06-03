

using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class RoleService : ReadOnlyService<Role>,IRoleService
    {
        public RoleService(IReadOnlyRepository<Role> readOnlyRepository) : base(readOnlyRepository)
        {
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await ((IRoleRepository)_readOnlyRepository).GetByNameAsync(name, cancellationToken);
        }
    }
}
