using Medicare.Application.Services.Interfaces;
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

        public ActionResult Index()
		{

			return View();
		}
	}
}
