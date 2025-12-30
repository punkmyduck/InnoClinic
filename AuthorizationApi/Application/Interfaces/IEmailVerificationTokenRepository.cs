using AuthorizationApi.Domain.Models;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IEmailVerificationTokenRepository
    {
        void Add(EmailVerificationToken token);
    }
}
