using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Results
{
    public class RegisterCommandResult : ICommandResult
    {
        public Guid AccountId { get; }
        public bool IsEmailVerificationRequired { get; }
        public RegisterCommandResult(Guid accountId, bool isEmailVerificationRequired)
        {
            AccountId = accountId;
            IsEmailVerificationRequired = isEmailVerificationRequired;
        }
    }
}