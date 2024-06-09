using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Users.Interfaces
{
    public interface ILoginUserUseCase
    {
        Task<User?> ExecuteAsync(User user, CancellationToken cancellationToken);

    }
}
