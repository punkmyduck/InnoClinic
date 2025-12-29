using AuthorizationApi.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationApi.Infrastructure.Security
{
    public class Sha256TokenHashGenerator : ITokenHashGenerator
    {
        public string GenerateHash(string rawToken)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(rawToken);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyToken(string rawToken, string tokenHash)
        {
            return GenerateHash(rawToken) == tokenHash;
        }
    }
}
