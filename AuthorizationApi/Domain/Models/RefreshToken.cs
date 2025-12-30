using AuthorizationApi.Domain.Exceptions;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Domain.Models
{
    public class RefreshToken
    {
        public RefreshTokenId Id { get; }
        public AccountId AccountId { get; }
        public TokenHash TokenHash { get; }
        public DateTime ExpiresAt { get; }
        public bool IsRevoked { get; private set; }

        internal RefreshToken(
            RefreshTokenId id,
            AccountId accountId,
            TokenHash tokenHash,
            DateTime expiresAt,
            bool isRevoked)
        {
            Id = id;
            AccountId = accountId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            IsRevoked = isRevoked;
        }

        public static RefreshToken CreateToken(
            RefreshTokenId id,
            AccountId accountId,
            TokenHash tokenHash,
            DateTime expiresAt)
        {
            return new RefreshToken(id, accountId, tokenHash, expiresAt, false);
        }

        public void Revoke()
        {
            if (IsRevoked)
                throw new TokenAlreadyRevokedException("This refresh token already revoked");

            IsRevoked = true;
        }

        public bool IsExpired(DateTime now) => now >= ExpiresAt;
    }
}
