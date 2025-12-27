using AuthorizationApi.Application.Abstractions;

namespace AuthorizationApi.Application.Commands
{
    public class RegisterCommand : ICommand
    {
        public string Email { get; }
        public string Password { get; }
        public string? PhoneNumber { get; }
        public RegisterCommand(string email, string password, string? phoneNumber)
        {
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
    }
}