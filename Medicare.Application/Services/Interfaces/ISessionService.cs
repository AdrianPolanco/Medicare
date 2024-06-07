
using Medicare.Application.Models;
using Medicare.Domain.Entities;

namespace Medicare.Application.Services.Interfaces
{
    public interface ISessionService
    {
        void SetSession(UserSessionInfo value);  
        UserSessionInfo GetSession();
        void RemoveSession();
        Task<User> GetCurrentUser();
    }
}
