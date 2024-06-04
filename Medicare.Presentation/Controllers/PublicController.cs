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

        private readonly IRegisterAdministratorUseCase _registerAdministratorUseCase;
        private readonly ILoginUserUseCase _loginUserUseCase;

        public PublicController(
            IRegisterAdministratorUseCase registerAdministratorUseCase,
            ILoginUserUseCase loginUserUseCase
            )
        {
            _registerAdministratorUseCase = registerAdministratorUseCase;
            _loginUserUseCase = loginUserUseCase;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) return View(loginViewModel);


            User userCredentials = new User
            {
                Username = loginViewModel.Username,
                Password = loginViewModel.Password
            };
            User? user = await _loginUserUseCase.ExecuteAsync(userCredentials, cancellationToken);

            if (user is not null) return RedirectToRoute(new {controller = "Authenticated", action = "Index"});

            ModelState.AddModelError("Username", "Usuario o contraseña incorrectos");

            return View(loginViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
