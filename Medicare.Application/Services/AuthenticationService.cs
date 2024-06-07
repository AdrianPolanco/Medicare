

using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Helpers;

namespace Medicare.Application.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IPasswordManager _passwordManager;
		private readonly IUserService _userService;
		private User? _user;

        public AuthenticationService(IPasswordManager passwordManager, IUserService userService)
        {
			_passwordManager = passwordManager;
			_userService = userService;
			_user = null;
        }

		public User? GetUser()
		{
			return _user;
		}

		public async Task<bool> LogIn(User user, CancellationToken cancellationToken)
		{
			User? existingUser = await _userService.UserExists(user.Username, cancellationToken);
			if(existingUser is null) return false;

			string password = user.Password;

			bool isValidPassword = _passwordManager.VerifyPassword(password, existingUser.Password);

			if (isValidPassword) { 
				_user = await _userService.GetByIdAsync(existingUser.Id, cancellationToken);
			} 

			return isValidPassword;
		}
	}
}
