using AuthorizationApi.Domain.Models;
using AuthorizationApi.Infrastructure.Entities;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Infrastructure.Mappers
{
    internal static class AccountMapper
    {
        public static AccountEntity ToEntity(Account domain)
        {
            return new AccountEntity
            {
                Id = domain.Id.Value,
                Email = domain.Email.Value,
                PasswordHash = domain.PasswordHash.Value,
                PhoneNumber = domain.PhoneNumber?.Value,
                IsEmailVerified = domain.IsEmailVerified,
                CreatedBy = domain.AuditInfo.CreatedBy,
                CreatedAt = domain.AuditInfo.CreatedAt,
                UpdatedBy = domain.AuditInfo.UpdatedBy,
                UpdatedAt = domain.AuditInfo.UpdatedAt
            };
        }

        public static Account ToDomain(AccountEntity entity)
        {
            return new Account(
                new AccountId(entity.Id),
                new Email(entity.Email),
                new PasswordHash(entity.PasswordHash),
                entity.PhoneNumber is null ? null : new PhoneNumber(entity.PhoneNumber),
                entity.IsEmailVerified,
                new AuditInfo(entity.CreatedAt, entity.CreatedBy, entity.UpdatedAt, entity.UpdatedBy));
        }
    }
}
