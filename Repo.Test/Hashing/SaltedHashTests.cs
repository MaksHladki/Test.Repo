using NUnit.Framework;
using Repo.Helpers.Hashing;

namespace Repo.Test.Hashing
{
    [TestFixture()]
    public class SaltedHashTests
    {
        [Test()]
        [TestCase("123456")]
        [TestCase("password")]
        [TestCase("qwerty")]
        [TestCase("dragon")]
        [TestCase("abc123")]
        public void SaltedHashTest_Positive(string password)
        {
            SaltedHash hasher = new SaltedHash(password);

            Assert.True(SaltedHash.Verify(password, hasher.Hash, hasher.Salt));
        }

        [TestCase("123456")]
        [TestCase("password")]
        [TestCase("qwerty")]
        [TestCase("dragon")]
        [TestCase("abc123")]
        public void SaltedHashTest_Negative(string password)
        {
            SaltedHash hasher = new SaltedHash(password);
            var salt = '+' + hasher.Salt;

            Assert.False(SaltedHash.Verify(password, hasher.Hash, salt));
        }
    }
}