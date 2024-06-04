

using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Interfaces
{
	public interface ILoginUserUseCase
	{
		Task<User?> ExecuteAsync(User user, CancellationToken cancellationToken);

	}
}
