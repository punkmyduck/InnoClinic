using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Results
{
    public class SignInCommandResult : ICommandResult
    {
        public bool IsSuccessful { get; }
        public SignInCommandResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
