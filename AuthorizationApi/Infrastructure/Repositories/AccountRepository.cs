using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.Repositories;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Infrastructure.Repositories
{
    //TODO: implement repository methods
    public class AccountRepository : IAccountRepository
    {
        private AuthDbContext _context;
        public AccountRepository(AuthDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetByEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
