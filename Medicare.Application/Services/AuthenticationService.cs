

using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Services;

namespace Medicare.Application.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IPasswordManager _passwordManager;
		private readonly IUserService _userService;

        public AuthenticationService(IPasswordManager passwordManager, IUserService userService)
        {
			_passwordManager = passwordManager;
			_userService = userService;
        }
        public async Task<bool> LogIn(User user, CancellationToken cancellationToken)
		{
			User? existingUser = await _userService.UserExists(user.Username, cancellationToken);
			if(existingUser is null) return false;

			string password = user.Password;

			bool isValidPassword = _passwordManager.VerifyPassword(password, existingUser.Password);

			return isValidPassword;
		}
	}
}
