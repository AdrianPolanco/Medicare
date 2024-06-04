using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Users
{
	public class LoginUserUseCase : ILoginUserUseCase
	{
		private readonly IAuthenticationService _authenticationService;

        public LoginUserUseCase(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<User?> ExecuteAsync(User user, CancellationToken cancellationToken)
		{
			await _authenticationService.LogIn(user, cancellationToken);
			return _authenticationService.GetUser();
		}
	}
}
