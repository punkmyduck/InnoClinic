using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Domain.Exceptions;

namespace AuthorizationApi.Domain.Models
{
    public class Account
    {
        public AccountId Id { get; }

        public Email Email { get; }
        public PasswordHash PasswordHash { get; }
        public PhoneNumber? PhoneNumber { get; }
        public bool IsEmailVerified { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public Account(
            AccountId id,
            Email email,
            PasswordHash passwordHash,
            PhoneNumber? phoneNumber,
            bool isEmailVerified,
            AuditInfo audit)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            IsEmailVerified = isEmailVerified;
            AuditInfo = audit;
        }

        public Account Create(
            AccountId id,
            Email email,
            PasswordHash passwordHash,
            PhoneNumber? phoneNumber,
            DateTime createdAt,
            Guid createdBy)
        {
            return new Account(
                id,
                email,
                passwordHash,
                phoneNumber,
                false,
                new AuditInfo(createdAt, createdBy, null, null));
        }

        public void VerifyEmail(Guid verifiedBy, DateTime verifiedAt)
        {
            if (IsEmailVerified)
                throw new EmailAlreadyVerifiedException($"User's {Id} email {Email} already verified");

            IsEmailVerified = true;
            AuditInfo = AuditInfo.WithUpdate(verifiedAt, verifiedBy);
        }
    }
}
