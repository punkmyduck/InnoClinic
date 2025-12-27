using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmailAsync(Email email);
        Task AddAsync(Account account);
    }
}
