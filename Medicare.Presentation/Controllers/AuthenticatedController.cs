using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
	[ServiceFilter(typeof(SessionAuthenticationFilter))]
	public class AuthenticatedController : Controller
	{
		private readonly ISessionService _sessionService;
		private readonly IUserService _userService;
        public AuthenticatedController(ISessionService sessionService, IUserService userService)
        {
            _sessionService = sessionService;
			_userService = userService;
        }

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
		{
			UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
			User user = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);
			
			return View(user);
		}

		public ActionResult LogOut()
		{
			_sessionService.RemoveSession(UserSessionInfo.UserSessionKey);
			return RedirectToAction("Index", "Public");
		}
	}
}
