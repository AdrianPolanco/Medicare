using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
    internal interface IRegisterAdministratorUseCase
    {
        Task<bool> ExecuteAsync(User user, string officeName, CancellationToken cancellationToken);
    }
}
