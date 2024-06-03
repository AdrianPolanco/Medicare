using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities;

namespace Medicare.Application.Services.Interfaces
{
    public interface IUserService: IService<User>
    {
        Task<bool> UserExists(string name, CancellationToken cancellationToken);
    }
}
