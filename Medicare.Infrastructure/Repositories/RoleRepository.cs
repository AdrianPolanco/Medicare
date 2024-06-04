
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Medicare.Infrastructure.Repositories
{
    public class RoleRepository: ReadOnlyRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(r => r.Users).FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        }
    }
}
