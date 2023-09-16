using System.Security.Cryptography;
using System.Text;
using Bookmark.Manager.Core.Helpers;
using Bookmark.Manager.Logic.Abstraction;
using Microsoft.Extensions.Options;

namespace Bookmark.Manager.Logic.Implementation
{
    public class EncryptionService : IEncryptionService
    {
        private readonly SecuritySettings _settings;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public EncryptionService(IOptions<SecuritySettings> settings)
        {
            _settings = settings.Value;
        }
        public string DecryptPassword(string password)
        {
            var salt = Encoding.ASCII.GetBytes(_settings.Salt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _settings.Iterations,
                hashAlgorithm,
                _settings.KeySize);
            return Convert.ToHexString(hash);
        }
    }
}