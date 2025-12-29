using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Domain.Exceptions;

namespace AuthorizationApi.Domain.Models
{
    public class Account
    {
        public AccountId Id { get; }

        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
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

        public static Account Create(
            AccountId id,
            Email email,
            PasswordHash passwordHash,
            PhoneNumber? phoneNumber,
            DateTime createdAt,
            AccountId createdBy)
        {
            return new Account(
                id,
                email,
                passwordHash,
                phoneNumber,
                false,
                new AuditInfo(createdAt, createdBy.Value, null, null));
        }

        public void VerifyEmail(Guid verifiedBy, DateTime verifiedAt)
        {
            if (IsEmailVerified)
                throw new EmailAlreadyVerifiedException($"User's {Id} email {Email} already verified");

            IsEmailVerified = true;
            AuditInfo = AuditInfo.WithUpdate(verifiedAt, verifiedBy);
        }

        public void ChangePassword(PasswordHash newHash, DateTime changedAt, Guid changedBy)
        {
            PasswordHash = newHash;
            AuditInfo = AuditInfo.WithUpdate(changedAt, changedBy);
        }

        public void ChangeEmail(Email newEmail, bool isNewEmailVerified, DateTime changedAt, Guid changedBy)
        {
            Email = newEmail;
            IsEmailVerified = isNewEmailVerified;
            AuditInfo = AuditInfo.WithUpdate(changedAt, changedBy);
        }

        public void ChangePhoneNumber(PhoneNumber newPhoneNumber, DateTime changedAt, Guid changedBy)
        {
            PhoneNumber = newPhoneNumber;
            AuditInfo = AuditInfo.WithUpdate(changedAt, changedBy);
        }
    }
}
