using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Results
{
    public class SignInCommandResult : ICommandResult
    {
        public bool IsSuccessful { get; }
        public string? JwtToken { get; }
        public DateTime? ExpiresAt { get; }
        public string? RefreshToken { get; }
        public SignInCommandResult(bool isSuccessful, string? jwtToken = null, DateTime? expiresAt = null, string? refreshToken = null)
        {
            IsSuccessful = isSuccessful;
            JwtToken = jwtToken;
            ExpiresAt = expiresAt;
            RefreshToken = refreshToken;
        }
    }
}
