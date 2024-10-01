using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace user.src.Utils
{
    // change to static
    static class PasswordUtils
    {
        public static void HashPassword(
                string originalPassword,
                out string hashedPassword,
                out byte[] salt)
        {
            var hmac = new HMACSHA256();
            salt = hmac.Key;
            hashedPassword = BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        }
        public static bool VerifyPassword(string originalPassword, string hashedPassword, byte[] salt)
        {
            var hmac = new HMACSHA256(salt);
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword))) == hashedPassword;
        }
    }
}