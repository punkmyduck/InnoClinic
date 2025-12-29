using AuthorizationApi.Domain.Models;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IEmailVerificationTokenRepository
    {
        Task AddAsync(EmailVerificationToken token, CancellationToken cancellationToken);
    }
}
