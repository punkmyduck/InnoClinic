using AuthorizationApi.Application.Interfaces;

namespace AuthorizationApi.Infrastructure.Security
{
    public class DefaultPasswordPolicy : IPasswordPolicy
    {
        public bool IsValid(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;

            return true;
        }
    }
}
