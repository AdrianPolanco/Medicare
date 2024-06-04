using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
    public interface IRegisterAdministratorUseCase
    {
        Task<bool> ExecuteAsync(User user, string officeName, CancellationToken cancellationToken);
    }
}
