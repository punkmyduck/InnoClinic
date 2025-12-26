namespace AuthorizationApi.Infrastructure.Entities
{
    public class EmailVerificationTokenEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string TokenHash { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
    }
}
