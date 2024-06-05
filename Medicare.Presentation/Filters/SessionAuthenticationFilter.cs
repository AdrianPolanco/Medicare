using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Presentation.Filters
{
    internal class SessionAuthenticationFilter : Attribute, IAsyncActionFilter
    {
        private readonly ISessionService _sessionService;
        public SessionAuthenticationFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UserSessionInfo userSession = _sessionService.GetSession("UserSession");

            if (userSession is null ||
                userSession.UserId == Guid.Empty ||
                userSession.OfficeId == Guid.Empty ||
                userSession.RoleId == Guid.Empty)
            {
                context.Result = new RedirectToActionResult("Index", "Public", null);
                return;
            }

            await next();
        }
    }
}
