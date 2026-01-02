using AuthorizationApi.Domain.ValueObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthorizationApi.Application.Secutiry
{
    public static class ClaimsPrincipalExtensions
    {
        public static AccountId GetAccountId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                ?? throw new UnauthorizedAccessException("Missing sub claim.");

            return new AccountId(Guid.Parse(value));
        }
    }
}
