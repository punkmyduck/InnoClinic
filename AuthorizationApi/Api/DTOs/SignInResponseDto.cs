namespace AuthorizationApi.Api.DTOs
{
    public record SignInResponseDto(
        bool IsSuccessful, 
        string? JwtToken, 
        DateTime? ExpiresAt, 
        string? RefreshToken);
}
