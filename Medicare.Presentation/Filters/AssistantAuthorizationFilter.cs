using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Medicare.Presentation.Filters
{
    internal class AssistantAuthorizationFilter : Attribute, IAsyncActionFilter
    {
        private readonly ISessionService _sessionService;
        private readonly IRoleService _roleService;
        public AssistantAuthorizationFilter(ISessionService sessionService, IRoleService roleService)
        {
            _roleService = roleService;
            _sessionService = sessionService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UserSessionInfo userSessionInfo = _sessionService.GetSession();
            Role assistantRole = await _roleService.GetByNameAsync("Asistente", new CancellationToken());

            if(userSessionInfo.RoleId != assistantRole.Id)
            {
                context.Result = new RedirectToActionResult("Index", "Admin", null);
                return;
            }

            await next();
        }
    }
}
