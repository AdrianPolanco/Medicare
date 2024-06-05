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

		public IActionResult LogOut()
		{
			_sessionService.RemoveSession(UserSessionInfo.UserSessionKey);
			return RedirectToAction("Index", "Public");
		}
	}
}
