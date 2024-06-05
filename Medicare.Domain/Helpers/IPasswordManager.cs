

namespace Medicare.Domain.Helpers
{
    public interface IPasswordManager
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}
