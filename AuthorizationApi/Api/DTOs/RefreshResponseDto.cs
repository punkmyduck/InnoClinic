namespace AuthorizationApi.Api.DTOs
{
    public record RefreshResponseDto(string? JwtToken, DateTime? ExpiresAt, string? RefreshToken);
}
