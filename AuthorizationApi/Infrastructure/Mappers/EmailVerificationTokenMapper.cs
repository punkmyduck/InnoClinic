using AuthorizationApi.Domain.Models;
using AuthorizationApi.Infrastructure.Entities;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Infrastructure.Mappers
{
    internal static class EmailVerificationTokenMapper
    {
        public static EmailVerificationTokenEntity ToEntity(EmailVerificationToken domain)
        {
            return new EmailVerificationTokenEntity
            {
                Id = domain.Id.Value,
                AccountId = domain.AccountId.Value,
                TokenHash = domain.TokenHash.Value,
                ExpiresAt = domain.ExpiresAt,
                IsUsed = domain.IsUsed
            };
        }

        public static EmailVerificationToken ToDomain(EmailVerificationTokenEntity entity)
        {
            return new EmailVerificationToken(
                new EmailVerificationTokenId(entity.Id),
                new AccountId(entity.AccountId),
                new TokenHash(entity.TokenHash),
                entity.ExpiresAt,
                entity.IsUsed
            );
        }
    }
}
