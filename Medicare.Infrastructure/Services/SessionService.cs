using Medicare.Application.Services.Interfaces;
using Medicare.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;


namespace Medicare.Infrastructure.Services
{
    public class SessionService<T> : ISessionService<T> where T : class
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccesor = httpContextAccessor;
        }
        public T GetSession(string key)
        {
            return _httpContextAccesor.HttpContext.Session.GetObject<T>(key);
        }

        public void RemoveSession(string key)
        {
            _httpContextAccesor.HttpContext.Session.Remove(key);
        }

        public void SetSession(string key, T value)
        {
            _httpContextAccesor.HttpContext.Session.SetObject(key, value);
        }
    }
}
