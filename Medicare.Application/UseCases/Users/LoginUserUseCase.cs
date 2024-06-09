using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Users.Interfaces;
using Medicare.Domain.Entities;

namespace Medicare.Application.UseCases.Users
{
    public class LoginUserUseCase : ILoginUserUseCase
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly ISessionService _sessionService;

        public LoginUserUseCase(IAuthenticationService authenticationService, ISessionService sessionService)
        {
            _authenticationService = authenticationService;
			_sessionService = sessionService;
        }
        public async Task<User?> ExecuteAsync(User user, CancellationToken cancellationToken)
		{
			await _authenticationService.LogIn(user, cancellationToken);
			User? authenticatedUser = _authenticationService.GetUser();

			if (authenticatedUser is null) return null;

			_sessionService.SetSession(new UserSessionInfo
			{
                UserId = authenticatedUser.Id,
                OfficeId = authenticatedUser.OfficeId,
                RoleId = authenticatedUser.RoleId,
                OwnsOffice = authenticatedUser.OwnedOfficeId != default,
				OwnerId = authenticatedUser.Office.OwnerId
            });

			return authenticatedUser;
		}
	}
}
