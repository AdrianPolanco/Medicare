
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Medicare.Infrastructure.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            //Generar una sal aleatoria para la contraseña
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //Crear una contraseña generada con HAMCSHA256 y 10000 iteraciones
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                 password: password,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 10000,
                 numBytesRequested: 256 / 8));

            //Retornar la sal separada de la contraseña por un punto y la contraseña hasheada como contraseña final
            return $"{Convert.ToBase64String(salt)}.{hashedPassword}";
        }

        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            //Separar la sal y la contraseña hasheada
            var parts = hashedPassword.Split('.', 2);
            if (parts.Length != 2) return false;

            //Obteniendo las dos partes de la contraseña hasheada
            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            //Generar la contraseña hasheada con la sal y la contraseña ingresada
            var providedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            //Comparar las contraseñas hasheadas
            return providedHash == hash;
        }
    }
}
