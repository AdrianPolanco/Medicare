

using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
    public interface IUpdateUserUseCase
    {
        Task<bool> ExecuteAsync(User user, CancellationToken cancellationToken);
    }
}
