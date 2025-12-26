namespace AuthorizationApi.Infrastructure.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string TokenHash { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}
