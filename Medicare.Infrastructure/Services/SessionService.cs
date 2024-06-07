using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;


namespace Medicare.Infrastructure.Services
{
    public class SessionService: ISessionService
    {

        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUserService _userService;
        public SessionService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccesor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<User> GetCurrentUser()
        {
            UserSessionInfo userSessionInfo = GetSession();
            return await _userService.GetByIdAsync(userSessionInfo.UserId, new CancellationToken());
        }

        public UserSessionInfo GetSession()
        {
            return _httpContextAccesor.HttpContext.Session.GetObject<UserSessionInfo>(UserSessionInfo.UserSessionKey);
        }

        public void RemoveSession()
        {
            _httpContextAccesor.HttpContext.Session.Remove(UserSessionInfo.UserSessionKey);
        }

        public void SetSession(UserSessionInfo value)
        {
            _httpContextAccesor.HttpContext.Session.SetObject(UserSessionInfo.UserSessionKey, value);
        }
    }
}
