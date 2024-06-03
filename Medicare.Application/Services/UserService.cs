using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class UserService: Service<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public override async Task<ICollection<User>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<User, bool>> filter = null, params Expression<Func<User, object>>[] includes)
        {
            return await base.GetByPagesAsync(page, cancellationToken, null, user => user.Role, user => user.Office, user => user.OwnedOffice);
        }

        public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes)
        {
            return await base.GetByIdAsync(id, cancellationToken, user => user.Role, user => user.Office, user => user.OwnedOffice);
        }

        public async Task<bool> UserExists(string name, CancellationToken cancellationToken)
        {
            return await ((IUserRepository)_repository).UserExists(name, cancellationToken);
        }
    }
}
