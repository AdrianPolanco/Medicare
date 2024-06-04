

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

		public override async Task<ICollection<Role>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<Role, bool>> filter = null, params Expression<Func<Role, object>>[] includes)
		{
			return await base.GetByPagesAsync(page, cancellationToken, null, role => role.Users);
		}

		public override async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<Role, object>>[] includes)
		{
			return await base.GetByIdAsync(id, cancellationToken, role => role.Users);
		}

		public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await ((IRoleRepository)_readOnlyRepository).GetByNameAsync(name, cancellationToken);
        }
    }
}
