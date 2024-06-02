

using Medicare.Infrastructure.Services;

namespace Medicare.Tests.Unit.Infrastructure
{
    public class PasswordHasherTest
    {
        [Theory]
        [InlineData("DoctorSlug582!")]
        [InlineData("·KmwurPowu74182!+")]
        [InlineData("MediCare74Kl!")]
        [InlineData("IsThisPasswordValid?")]
        public void Password_Should_Be_Valid(string password)
        {
            PasswordManager passwordHasher = new PasswordManager();
            string hashedPassword = passwordHasher.HashPassword(password);
            bool isValid = passwordHasher.VerifyPassword(password, hashedPassword);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("DoctorSlug582!", "WrongSlug582!")]
        [InlineData("·KmwurPowu74182!+", "·KmwrPowu74182!+")]
        [InlineData("MediCare74Kl!", "MediCure74Kl!")]
        [InlineData("IsThisPasswordValid?", "IsThisPasswordInval1d?")]
        [InlineData("SecurePass123$", "Secur3Pass123$")]
        [InlineData("!StrongPass!789", "!StrongPa55!789")]
        [InlineData("Pa$$w0rdChange1!", "Pa$$w0rdChange2!")]
        [InlineData("VerifyThisOne99%", "VerifyThisTwo99%")]
        [InlineData("EncryptionTest!12", "EncryptionTest!13")]
        [InlineData("HashMeIfYouCan34!", "HashMeIfYouCant34!")]
        public void Password_Should_Not_Be_Valid(string password, string wrongPassword)
        {
            PasswordManager passwordHasher = new PasswordManager();
            string hashedPassword = passwordHasher.HashPassword(password);
            bool isValid = passwordHasher.VerifyPassword(wrongPassword, hashedPassword);
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("DoctorSlug582!", "doctorslug582!")]
        [InlineData("·KmwurPowu74182!+", "·kmwrPowu74182!+")]
        [InlineData("MediCare74Kl!", "Medicare74Kl!")]
        [InlineData("IsThisPasswordValid?", "isThisPasswordvalid?")]
        [InlineData("SecurePass123$", "securepass123$")]
        [InlineData("!StrongPass!789", "!strongpass!789")]
        [InlineData("Pa$$w0rdChange1!", "pa$$w0rdChange1!")]
        [InlineData("VerifyThisOne99%", "verifythisOne99%")]
        [InlineData("EncryptionTest!12", "encryptiontest!12")]
        [InlineData("HashMeIfYouCan34!", "hashmeIfYouCan34!")]
        public void Password_Is_Case_Sensitive(string password, string wrongPassword)
        {
            PasswordManager passwordHasher = new PasswordManager();
            string hashedPassword = passwordHasher.HashPassword(password);
            bool isValid = passwordHasher.VerifyPassword(wrongPassword.ToLower(), hashedPassword);
            Assert.False(isValid);
        }
    }
}
