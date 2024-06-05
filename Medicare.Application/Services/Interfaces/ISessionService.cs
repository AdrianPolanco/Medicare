
namespace Medicare.Application.Services.Interfaces
{
    public interface ISessionService<T> where T : class
    {
        void SetSession(string key, T value);  
        T GetSession(string key);
        void RemoveSession(string key);
    }
}
