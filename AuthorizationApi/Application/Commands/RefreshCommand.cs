using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Commands
{
    public class RefreshCommand : ICommand
    {
        public string RefreshToken { get; }
        public RefreshCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
