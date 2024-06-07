using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Medicare.Presentation.Filters
{
    internal class AdminAuthorizationFilter : Attribute, IAsyncActionFilter
    {
        private readonly ISessionService _sessionService;
        private readonly IRoleService _roleService;
        public AdminAuthorizationFilter(ISessionService sessionService, IRoleService roleService)
        {
            _roleService = roleService;
            _sessionService = sessionService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UserSessionInfo userSession = _sessionService.GetSession();
            Role adminRole = await _roleService.GetByNameAsync("Administrador", new CancellationToken());

            if(userSession.RoleId != adminRole.Id)
            {
                context.Result = new RedirectToActionResult("Index", "Assistant", null);
                return;
            }

            await next();
        }
    }
}
