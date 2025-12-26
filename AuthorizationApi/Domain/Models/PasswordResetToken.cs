using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Domain.Exceptions;

namespace AuthorizationApi.Domain.Models
{
    public class PasswordResetToken
    {
        public PasswordResetTokenId Id { get; }

        public AccountId AccountId { get; }
        public TokenHash TokenHash { get; }
        public DateTime ExpiresAt { get; }
        public bool IsUsed { get; private set; }

        private PasswordResetToken(
            PasswordResetTokenId id,
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

        public PasswordResetToken CreateToken(
            PasswordResetTokenId id,
            AccountId accountId,
            TokenHash tokenHash,
            DateTime expiresAt)
        {
            return new PasswordResetToken(id, accountId, tokenHash, expiresAt, false);
        }

        public void MarkAsUsed()
        {
            if (IsUsed)
                throw new PasswordResetTokenAlreadyUsedException("Password reset token has already been used.");

            IsUsed = true;
        }

        public bool IsExpired(DateTime now) => now >= ExpiresAt;
    }
}
