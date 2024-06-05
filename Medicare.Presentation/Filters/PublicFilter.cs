using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Medicare.Presentation.Filters
{
    public class PublicFilter : Attribute, IAuthorizationFilter
    {
        private readonly ISessionService _sessionService;

        public PublicFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            UserSessionInfo userSessionInfo = _sessionService.GetSession(UserSessionInfo.UserSessionKey);

            if( userSessionInfo != null 
                && userSessionInfo.UserId != Guid.Empty
                && userSessionInfo.OfficeId != Guid.Empty
                && userSessionInfo.RoleId != Guid.Empty) context.Result = new RedirectToActionResult("Index", "Admin", null);
        }
    }
}
