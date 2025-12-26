using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Domain.Exceptions;

namespace AuthorizationApi.Domain.Models
{
    public class EmailVerificationToken
    {
        public EmailVerificationTokenId Id { get; }

        public AccountId AccountId { get; }
        public TokenHash TokenHash { get; }
        public DateTime ExpiresAt { get; }
        public bool IsUsed { get; private set; }

        private EmailVerificationToken(
            EmailVerificationTokenId id,
            AccountId accountId,
            TokenHash tokenHash,
            DateTime expiresAt,
            bool isUsed)
        {
            Id = id;
            AccountId = accountId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            IsUsed = isUsed;
        }

        public static EmailVerificationToken CreateToken(
            EmailVerificationTokenId id,
            AccountId accountId,
            TokenHash tokenHash,
            DateTime expiresAt)
        {
            return new EmailVerificationToken(id, accountId, tokenHash, expiresAt, false);
        }

        public void MarkAsUsed()
        {
            if (IsUsed)
                throw new EmailVerificationTokenAlreadyUsedException("This email verification token already used.");

            IsUsed = true;
        }

        public bool IsExpired(DateTime now) => now >= ExpiresAt;
    }
}
