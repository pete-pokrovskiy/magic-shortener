using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Common.Security
{
    /// <summary>
    /// Класс-помощник для хэширования пароля
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        public (string hash, string password) Hash(string password)
        {
            // генерируем соль
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            // с помощью 1000 итераций алгоритма HMACSHA1 формируем хэш
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (hash, saltString);
        }

        public bool Check(byte[] hash, byte[] salt, string password)
        {
            string passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Convert.ToBase64String(hash) == passwordHash;
        }
    }
}
