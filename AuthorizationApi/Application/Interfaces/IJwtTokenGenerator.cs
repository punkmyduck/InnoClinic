using AuthorizationApi.Application.ValueObjects;
using AuthorizationApi.Domain.ValueObjects;
using System.Security.Claims;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        JwtTokenResult GenerateAccessToken(
            IReadOnlyCollection<Claim> claims,
            DateTime expiresAt);

        JwtTokenResult GenerateRefreshToken(
            AccountId accountId,
            DateTime expiresAt);
    }
}
