using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Infrastructure.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmailAsync(Email email);
        Task AddAsync(Account account);
    }
}
