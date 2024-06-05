using Medicare.Application.Models;
using Medicare.Application.Services.Interfaces;
using Medicare.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;


namespace Medicare.Infrastructure.Services
{
    public class SessionService: ISessionService
    {

        private readonly IHttpContextAccessor _httpContextAccesor;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesor = httpContextAccessor;
        }
        public UserSessionInfo GetSession(string key)
        {
            return _httpContextAccesor.HttpContext.Session.GetObject<UserSessionInfo>(key);
        }

        public void RemoveSession(string key)
        {
            _httpContextAccesor.HttpContext.Session.Remove(key);
        }

        public void SetSession(string key, UserSessionInfo value)
        {
            _httpContextAccesor.HttpContext.Session.SetObject(key, value);
        }
    }
}
