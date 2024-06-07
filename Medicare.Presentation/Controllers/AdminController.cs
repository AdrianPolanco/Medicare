using Medicare.Application.Enums;
using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Helpers;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Medicare.Presentation.Controllers
{
    [ServiceFilter(typeof(SessionAuthenticationFilter))]
    public class AdminController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRegisterUserFromAdminAccountUseCase _registerUserFromAdminAccountUseCase;

        public AdminController(
            ISessionService sessionService, 
            IUserService userService, 
            IRoleService roleService,
            IRegisterUserFromAdminAccountUseCase registerUserFromAdminAccountUseCase
            )
        {
            _sessionService = sessionService;
            _userService = userService;
            _roleService = roleService;
            _registerUserFromAdminAccountUseCase = registerUserFromAdminAccountUseCase;
        }
        public async Task<IActionResult> Index(int? page, string search, UserFilterOptions? option, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<User, bool>> searchFilter = FilterHelper.GetUserFilter(option, search, userSessionInfo.OfficeId);
            ICollection<User> recoveredUsers = await _userService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<User> users = recoveredUsers.ToList();
            UsersMenuViewModel usersMenuViewModel = new UsersMenuViewModel { 
                Users = users, 
                Admins = UserFilterOptions.Admins, 
                Pages = (int)page 
            };
            return View(usersMenuViewModel);
        }

        public async Task<IActionResult> CreateNewUser(CancellationToken cancellationToken)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewUser(RegisterUserFromAdminUserViewModel registerUserFromAdminUserViewModel, CancellationToken cancellationToken)
        {
            User currentUser = await _sessionService.GetCurrentUser();
            ICollection<Role> rolesCollection = await _roleService.GetByPagesAsync(1, cancellationToken);

            User? userExists = await _userService.UserExists(registerUserFromAdminUserViewModel.Username, cancellationToken);
            if(userExists is not null)
            {
                ModelState.AddModelError("Username", "El nombre de usuario ya existe");
                return View("CreateNewUser", registerUserFromAdminUserViewModel);
            }
              if (!ModelState.IsValid)
              {
                  return View("CreateNewUser", registerUserFromAdminUserViewModel);
              }

              User user = new User
              {
                  Name = registerUserFromAdminUserViewModel.Name,
                  Lastname = registerUserFromAdminUserViewModel.Lastname,
                  Username = registerUserFromAdminUserViewModel.Username,
                  Email = registerUserFromAdminUserViewModel.Email,
                  Password = registerUserFromAdminUserViewModel.Password,
                  RoleId = registerUserFromAdminUserViewModel.RoleId,
                  OfficeId = currentUser.OfficeId
              };

              await _registerUserFromAdminAccountUseCase.ExecuteAsync(user, cancellationToken);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUser(Guid id, CancellationToken cancellationToken)
        {
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            User userToEdit = await _userService.GetByIdAsync(id, cancellationToken);
            ICollection<Role> rolesCollection = await _roleService.GetByPagesAsync(1, cancellationToken);

            UpdateUserFromAdminViewModel updateUserFromAdminUserViewModel = new UpdateUserFromAdminViewModel
            {
                Id = userToEdit.Id,
                Name = userToEdit.Name,
                Lastname = userToEdit.Lastname,
                Username = userToEdit.Username,
                Email = userToEdit.Email,
                OfficeName = userToEdit.Office.Name,
            };

            return View(updateUserFromAdminUserViewModel);
        }
    }
}
