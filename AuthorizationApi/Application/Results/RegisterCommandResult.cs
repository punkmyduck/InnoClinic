using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Results
{
    public class RegisterCommandResult : ICommandResult
    {
        public Guid AccountId { get; }
        public RegisterCommandResult(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}