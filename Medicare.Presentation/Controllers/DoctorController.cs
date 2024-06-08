using Medicare.Presentation.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
