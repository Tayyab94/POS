using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Helpers
{
   public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Simple hash for demonstration purposes. Use a stronger hashing algorithm in production.
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return StringComparer.Ordinal.Compare(hashOfInput, hashedPassword) == 0;
        }
    }
}
