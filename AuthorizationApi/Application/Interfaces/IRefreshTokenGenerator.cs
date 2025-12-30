using AuthorizationApi.Application.ValueObjects;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IRefreshTokenGenerator
    {
        JwtTokenResult GenerateRefreshToken(
            AccountId accountId,
            DateTime expiresAt);
    }
}
