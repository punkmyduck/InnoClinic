using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Application.ValueObjects;
using AuthorizationApi.Domain.ValueObjects;
using System.Security.Cryptography;

namespace AuthorizationApi.Infrastructure.Security
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public JwtTokenResult GenerateRefreshToken(AccountId accountId, DateTime expiresAt)
        {
            var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return new JwtTokenResult(raw, expiresAt);
        }
    }
}
