using AuthorizationApi.Application.Interfaces;

namespace AuthorizationApi.Infrastructure.Security
{
    public class BCryptPasswordHashed : IPasswordHasher
    {
        public string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
