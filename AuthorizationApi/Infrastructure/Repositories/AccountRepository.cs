using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private AuthDbContext _context;
        public AccountRepository(AuthDbContext context)
        {
            _context = context;
        }
        public void Add(Account account)
        {
            var entity = AccountMapper.ToEntity(account);
            _context.Accounts.Add(entity);
        }

        public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                .AnyAsync(x => x.Email == email.Value, cancellationToken);
        }

        public async Task<Account?> GetByAccountIdAsync(AccountId accoundId, CancellationToken cancellationToken)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == accoundId.Value);

            if (account is null) return null;

            return AccountMapper.ToDomain(account);
        }

        public async Task<Account?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            var entity = await _context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email.Value, cancellationToken);
            if (entity is null) return null;
            return AccountMapper.ToDomain(entity);
        }
    }
}
