using AuthorizationApi.Domain.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationApi.Infrastructure.Security
{
    public static class JwtClaimsFactory
    {
        public static IReadOnlyCollection<Claim> Create(Account account, string issuer)
        {
            var now = DateTimeOffset.UtcNow;

            return new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, account.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.Email, account.Email.Value),
                new(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Iss, issuer)
            };
        }
    }
}
