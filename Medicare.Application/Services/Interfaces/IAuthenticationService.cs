

using Medicare.Domain.Entities;

namespace Medicare.Application.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<bool> LogIn(User user, CancellationToken cancellationToken);
		User? GetUser();
	}
}
