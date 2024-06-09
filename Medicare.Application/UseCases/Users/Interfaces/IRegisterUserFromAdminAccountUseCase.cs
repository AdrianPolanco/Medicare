using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface IRegisterUserFromAdminAccountUseCase
    {
        Task<bool> ExecuteAsync(User user, CancellationToken cancellationToken);
    }
}
