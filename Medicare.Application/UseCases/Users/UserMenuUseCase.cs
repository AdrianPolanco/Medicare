
using Medicare.Application.Enums;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using System.Linq.Expressions;

namespace Medicare.Application.UseCases.Users
{
    public class UserMenuUseCase : IUsersMenuUseCase
    {
        public UserMenuUseCase(IUserService userService, ISessionService sessionService)
        {
            
        }

        public Task<bool> ExecuteAsync(int page, UserFilterOptions filterOptions, Expression<Func<User, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
