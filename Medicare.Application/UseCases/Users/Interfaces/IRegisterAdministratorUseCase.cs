using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface IRegisterAdministratorUseCase
    {
        Task<bool> ExecuteAsync(User user, string officeName, CancellationToken cancellationToken);
    }
}
