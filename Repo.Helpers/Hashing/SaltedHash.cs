using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Repo.Helpers.Hashing
{
    public sealed class SaltedHash
    {
        #region Fields

        private const uint SaltLength = 64;

        #endregion

        #region Constructors

        public SaltedHash(string password)
        {
            Salt = GenerateSalt(SaltLength);
            Hash = ComputeHash(password, Salt);
        }

        #endregion

        #region Properties

        public string Hash { get; }
        public string Salt { get; }

        #endregion

        #region Methods

        public static bool Verify(string password, string hash, string salt)
        {
            var hashAttempt = ComputeHash(password, salt);
            return hash == hashAttempt;
        }

        static string ComputeHash(string password, string saltBase64)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(saltBase64);
            var passwordAndSaltBytes = passwordBytes.Concat(saltBytes).ToArray();

            using (var sha512 = SHA512.Create())
            {
                return Convert.ToBase64String(sha512.ComputeHash(passwordAndSaltBytes));
            }
        }

        string GenerateSalt(uint length)
        {
            var saltBytes = new byte[length];
            new Random().NextBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        #endregion
    }
}
