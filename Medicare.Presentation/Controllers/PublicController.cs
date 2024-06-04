using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Models;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Medicare.Presentation.Controllers
{
    public class PublicController : Controller
    {
        private readonly ILogger<PublicController> _logger;
        private readonly IRegisterAdministratorUseCase _registerAdministratorUseCase;

        public PublicController(ILogger<PublicController> logger, IRegisterAdministratorUseCase registerAdministratorUseCase)
        {
            _logger = logger;
            _registerAdministratorUseCase = registerAdministratorUseCase;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) return View("SignUp", registerUserViewModel);

            User user = new User
            {
                Name = registerUserViewModel.Name,
                Lastname = registerUserViewModel.Lastname,
                Username = registerUserViewModel.Username,
                Email = registerUserViewModel.Email,
                Password = registerUserViewModel.Password
            };

            bool success = await _registerAdministratorUseCase.ExecuteAsync(user, registerUserViewModel.OfficeName, cancellationToken);

            if(!success)
            {
                ModelState.AddModelError("Username", "El nombre de usuario ya está en uso");
                return View("SignUp", registerUserViewModel);
            }

            return RedirectToAction("Success");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
