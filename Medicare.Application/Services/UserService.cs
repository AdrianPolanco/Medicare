using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;

namespace Medicare.Application.Services
{
    public class UserService: Service<User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
