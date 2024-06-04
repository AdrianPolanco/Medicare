using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities;

namespace Medicare.Application.Services.Interfaces
{
    public interface IUserService: IService<User>
    {
        Task<User?> UserExists(string username, CancellationToken cancellationToken);
    }
}
