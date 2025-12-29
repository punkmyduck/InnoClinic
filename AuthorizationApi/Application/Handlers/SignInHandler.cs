using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;

namespace AuthorizationApi.Application.Handlers
{
    public class SignInHandler : ICommandHandler<SignInCommand, SignInCommandResult>
    {
        public Task<SignInCommandResult> HandleAsync(SignInCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
