using Medicare.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AssistantAuthorizationFilter))]
    public class AssistantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
