﻿using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Application.UseCases.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Presentation.Filters;
using Medicare.Presentation.Models.Users;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var recoveredRoles = await _roleService.GetByPagesAsync(1, cancellationToken);
            UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
            User user = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);
            AuthenticatedViewModel authenticatedViewModel = new AuthenticatedViewModel { CurrentUser = user, Roles = recoveredRoles.ToList() };
            return View(authenticatedViewModel);
        }

        public async Task<IActionResult> CreateNewUser(CancellationToken cancellationToken)
        {
            UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
            User user = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);
            ICollection<Role> rolesCollection = await _roleService.GetByPagesAsync(1, cancellationToken);
            List<Role> roles = rolesCollection.ToList();

            RegisterUserFromAdminUserViewModel registerUserFromAdminUserViewModel
                = new RegisterUserFromAdminUserViewModel { CurrentUser = user, Roles = roles };

            return View(registerUserFromAdminUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewUser(RegisterUserFromAdminUserViewModel registerUserFromAdminUserViewModel, CancellationToken cancellationToken)
        {
              if (!registerUserFromAdminUserViewModel.IsRegisterUserViewModelValid())
              {
                ICollection<Role> rolesCollection = await _roleService.GetByPagesAsync(1, cancellationToken);

                  List<Role> roles = rolesCollection.ToList();
                  registerUserFromAdminUserViewModel.Roles = roles;

                  UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);
                  User currentUser = await _userService.GetByIdAsync(userSessionInfo.UserId, cancellationToken);

                  registerUserFromAdminUserViewModel.CurrentUser = currentUser;

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
                  OfficeId = registerUserFromAdminUserViewModel.CurrentUser.OfficeId
              };

              await _registerUserFromAdminAccountUseCase.ExecuteAsync(user, cancellationToken);

            return RedirectToAction("Index");
        }
    }
}
