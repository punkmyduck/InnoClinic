using AuthorizationApi.Domain.Models;
using AuthorizationApi.Infrastructure.Entities;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Infrastructure.Mappers
{
    public static class RefreshTokenMapper
    {
        public static RefreshTokenEntity ToEntity(RefreshToken domain)
        {
            return new RefreshTokenEntity
            {
                Id = domain.Id.Value,
                AccountId = domain.AccountId.Value,
                TokenHash = domain.TokenHash.Value,
                ExpiresAt = domain.ExpiresAt,
                IsRevoked = domain.IsRevoked
            };
        }

        public static RefreshToken ToDomain(RefreshTokenEntity entity)
        {
            return new RefreshToken(
                new RefreshTokenId(entity.Id), 
                new AccountId(entity.AccountId), 
                new TokenHash(entity.TokenHash), 
                entity.ExpiresAt, 
                entity.IsRevoked);
        }
    }
}
