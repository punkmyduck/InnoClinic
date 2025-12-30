namespace AuthorizationApi.Api.DTOs
{
    public record SignInRequestDto(
        string Email,
        string Password
        );
}
