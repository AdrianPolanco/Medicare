using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
	public class AuthenticatedController : Controller
	{
		// GET: AuthenticatedController
		public ActionResult Index()
		{
			return View();
		}
	}
}
