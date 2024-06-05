using Medicare.Application.Services;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Helpers;
using Medicare.Infrastructure.Services;
using Moq;

namespace Medicare.Tests.Unit.Application
{
	public class AuthenticationServiceTests
	{
		private Mock<IUserService> _userServiceMock;
		private IPasswordManager _passwordManager;
		private IAuthenticationService _authenticationService;
		string basePassword = "\"AdrianDev777!\"";
		string baseUsername = "adrian_dev777";
		User recoveredUser;

        public AuthenticationServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
			_passwordManager = new PasswordManager();
			_authenticationService = new AuthenticationService(_passwordManager,_userServiceMock.Object);
			recoveredUser = new User
			{
				Username = baseUsername,
				Password = _passwordManager.HashPassword(basePassword)
			};
			_userServiceMock.Setup(userService => userService.UserExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
	.ReturnsAsync(recoveredUser);
		}

		[Fact]
		public async Task LogIn_When_UserExists_And_HasValidCredentials()
		{
			
			User user = new User
			{
				Username = baseUsername,
				Password = basePassword
			};

			bool result = await _authenticationService.LogIn(user, new CancellationToken());

			Assert.True(result);
		}

		[Theory]
		[InlineData("adrianDev777!")]
		[InlineData("adrian_dev777")]
		[InlineData("AdrianDev777")]
		[InlineData("NotValidPassword")]
		[InlineData("AdrianDev777! ")]
		[InlineData("")]
		public async Task LogIn_When_UserExists_And_HasInvalidPassword(string password)
		{

			User user = new User
			{
				Username = baseUsername,
				Password = password
			};

			bool result = await _authenticationService.LogIn(user, new CancellationToken());
			Assert.False(result);
		}


		[Theory]
		//[InlineData("adrian")]
		[InlineData("leonardo_taveras")]
		[InlineData("juancito")]
		[InlineData("NotValidUser")]
		//[InlineData("adrian_dev777 ")]
		[InlineData("")]
		public async Task LogIn_When_UserDoesntExist_And_HasValidPassword(string username)
		{
			User userNotFound = null;
			_userServiceMock.Setup(userService => userService.UserExists(It.IsAny<string>(), It.IsAny<CancellationToken>()))
	.ReturnsAsync(userNotFound);
			User user = new User
			{
				Username = username,
				Password = basePassword
			};

			bool result = await _authenticationService.LogIn(user, new CancellationToken());
			Assert.False(result);
		}

		[Theory]
		[InlineData("adrian", "Jamskwk")]
		[InlineData("leonardo_taveras", "profesor")]
		[InlineData("juancito", "sport")]
		[InlineData("NotValidUser", "NotValidPassword")]
		[InlineData("adrian_dev777 ", "AdrianDev777! ")]
		[InlineData("", "")]
		public async Task LogIn_When_InvalidCredentials(string username, string password)
		{
			User user = new User
			{
				Username = username,
				Password = password
			};

			bool result = await _authenticationService.LogIn(user, new CancellationToken());
			Assert.False(result);
		}
	}
}
