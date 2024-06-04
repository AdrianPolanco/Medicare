using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Services;
using Medicare.Infrastructure.Services;
using Moq;

namespace Medicare.Tests.Unit.Application
{
	public class AuthenticationServiceTests
	{
		private Mock<IUserService> _userServiceMock;
		private IPasswordManager _passwordManager;
		private IAuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
			_passwordManager = new PasswordManager();
			_authenticationService = new AuthenticationService(_passwordManager,_userServiceMock.Object);
        }

		[Fact]
		public async Task LogIn_When_UserExists_And_HasValidCredentials()
		{
			string basePassword = "\"AdrianDev777!\"";
			User recoveredUser = new User { 				
				Username = "adrian_dev777",
				Password = _passwordManager.HashPassword(basePassword)
			};

			User user = new User
			{
				Username = "adrian_dev777",
				Password = basePassword
			};

			_userServiceMock.Setup(userService => userService.UserExists(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(recoveredUser);
			bool result = await _authenticationService.LogIn(user, new CancellationToken());

			Assert.True(result);
		}
	}
}
