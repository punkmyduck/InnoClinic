namespace AuthorizationApi.Api.DTOs
{
    public record RegisterResponseDto(
        Guid AccountId,
        bool EmailVerificationRequired);
}