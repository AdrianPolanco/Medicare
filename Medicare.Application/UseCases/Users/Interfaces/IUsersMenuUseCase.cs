using Medicare.Application.Enums;
using Medicare.Domain.Entities;
using System.Linq.Expressions;

namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface IUsersMenuUseCase
    {
        Task<bool> ExecuteAsync(int page, UserFilterOptions filterOptions, Expression<Func<User, bool>> filter, CancellationToken cancellationToken);
    }
}
