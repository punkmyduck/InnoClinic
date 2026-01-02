using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Results
{
    public class RefreshCommandResult : ICommandResult
    {
        public string? JwtToken { get; }
        public DateTime? ExpiresAt { get; }
        public string? RefreshToken { get; }
        public RefreshCommandResult(
            string? jwtToken, 
            DateTime? expiresAt, 
            string? refreshToken)
        {
            JwtToken = jwtToken;
            ExpiresAt = expiresAt;
            RefreshToken = refreshToken;
        }
    }
}
