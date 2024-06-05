using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Medicare.Presentation.Controllers
{
	[ServiceFilter(typeof(SessionAuthenticationFilter))]
	public class AuthenticatedController : Controller
	{
		private readonly ISessionService _sessionService;
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private List<Role> roles;
        public AuthenticatedController(ISessionService sessionService, IUserService userService, IRoleService roleService)
        {
            _sessionService = sessionService;
			_userService = userService;
			_roleService = roleService;	
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
            var recoveredRoles = await _roleService.GetByPagesAsync(1, cancellationToken);
			roles = recoveredRoles.ToList();
            UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
			User user = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);			
			AuthenticatedViewModel authenticatedViewModel = new AuthenticatedViewModel(user, roles);
			return View(authenticatedViewModel);
		}

		public IActionResult LogOut()
		{
			_sessionService.RemoveSession(UserSessionInfo.UserSessionKey);
			return RedirectToAction("Index", "Public");
		}

		public async Task<IActionResult> CreateNewUser(CancellationToken cancellationToken)
		{
            UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
			User user = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);
			ICollection<Role> rolesCollection = await _roleService.GetByPagesAsync(1, cancellationToken);
			List<Role> roles = rolesCollection.ToList();

			RegisterUserFromAdminUserViewModel registerUserFromAdminUserViewModel 
				= new RegisterUserFromAdminUserViewModel(user, roles);
			registerUserFromAdminUserViewModel.OfficeName = user.Office.Name;

            return View(registerUserFromAdminUserViewModel);
		}
	}
}
