using Medicare.Presentation.Filters;
using Medicare.Presentation.Models.Doctors;
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


        public IActionResult CreateNewDoctor()
        {
            return View();
        }

        public async Task<IActionResult> SaveNewDoctor(CreateDoctorViewModel doctorViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return View("CreateNewDoctor", doctorViewModel);

            return View("Index");
        }   
    }
}
