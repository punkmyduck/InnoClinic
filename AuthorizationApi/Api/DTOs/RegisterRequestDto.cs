namespace AuthorizationApi.Api.DTOs
{
    public record RegisterRequestDto(
        string Email,
        string Password,
        string? PhoneNumber
    );
}