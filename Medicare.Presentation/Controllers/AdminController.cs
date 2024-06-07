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
    [ServiceFilter(typeof(AdminAuthorizationFilter))]
    public class AdminController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IRegisterUserFromAdminAccountUseCase _registerUserFromAdminAccountUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;

        public AdminController(
            ISessionService sessionService, 
            IUserService userService, 
            IRoleService roleService,
            IRegisterUserFromAdminAccountUseCase registerUserFromAdminAccountUseCase,
            IUpdateUserUseCase updateUserUseCase
            )
        {
            _sessionService = sessionService;
            _userService = userService;
            _roleService = roleService;
            _registerUserFromAdminAccountUseCase = registerUserFromAdminAccountUseCase;
            _updateUserUseCase = updateUserUseCase;
        }
        public async Task<IActionResult> Index(int? page, string search, UserFilterOptions? option, CancellationToken cancellationToken)
        {
            page = page ?? 1;
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Expression<Func<User, bool>> searchFilter = FilterHelper.GetUserFilter(option, search, userSessionInfo.OfficeId);
            ICollection<User> recoveredUsers = await _userService.GetByPagesAsync((int)page, cancellationToken, searchFilter);
            List<User> users = recoveredUsers.ToList();
            int pages = await _userService.GetRowsCountAsync(cancellationToken);
            UsersMenuViewModel usersMenuViewModel = new UsersMenuViewModel { 
                Users = users, 
                Admins = UserFilterOptions.Admins, 
                Pages = pages,
                CurrentPage = (int)page
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
            User userToEdit = await _userService.GetByIdAsync(id, cancellationToken);

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

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserFromAdminViewModel updateUserFromAdminViewModel, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid) return View("EditUser", updateUserFromAdminViewModel);

            User currentUser = await _sessionService.GetCurrentUser();

            User user = new User
            {
                Id = updateUserFromAdminViewModel.Id,
                Name = updateUserFromAdminViewModel.Name,
                Lastname = updateUserFromAdminViewModel.Lastname,
                Username = updateUserFromAdminViewModel.Username,
                Email = updateUserFromAdminViewModel.Email,
                RoleId = updateUserFromAdminViewModel.RoleId,
                Password = updateUserFromAdminViewModel.Password,
                OfficeId = currentUser.OfficeId
            };

            await _updateUserUseCase.ExecuteAsync(user, cancellationToken);

            return RedirectToAction("Index");
        }   

        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(userId, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
