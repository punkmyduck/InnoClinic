using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.Repositories;
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
        public async Task AddAsync(Account account)
        {
            var entity = AccountMapper.ToEntity(account);
            await _context.Accounts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetByEmailAsync(Email email)
        {
            var entity = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email.Value);
            if (entity is null) return null;
            return AccountMapper.ToDomain(entity);
        }
    }
}
