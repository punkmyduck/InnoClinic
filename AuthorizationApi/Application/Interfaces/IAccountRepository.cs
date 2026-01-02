using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
        Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
        Task<Account?> GetByAccountIdAsync(AccountId accoundId, CancellationToken cancellationToken);
        void Add(Account account);
    }
}
