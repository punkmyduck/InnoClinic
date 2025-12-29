namespace AuthorizationApi.Application.ValueObjects
{
    public record JwtTokenResult(string token, DateTime expiresAt);
}
