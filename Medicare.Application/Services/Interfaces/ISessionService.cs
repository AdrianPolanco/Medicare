
using Medicare.Application.Models;

namespace Medicare.Application.Services.Interfaces
{
    public interface ISessionService
    {
        void SetSession(string key, UserSessionInfo value);  
        UserSessionInfo GetSession(string key);
        void RemoveSession(string key);
    }
}
