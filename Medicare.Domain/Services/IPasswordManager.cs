

namespace Medicare.Domain.Services
{
    public interface IPasswordManager
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}
