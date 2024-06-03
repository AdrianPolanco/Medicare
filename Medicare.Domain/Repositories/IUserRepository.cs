using Medicare.Domain.Entities;
using Medicare.Domain.Repositories.Base;

namespace Medicare.Domain.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<bool> UserExists(string name, CancellationToken cancellationToken);
    }
}
