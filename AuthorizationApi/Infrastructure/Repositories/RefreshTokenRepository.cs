using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext _context;
        public RefreshTokenRepository(AuthDbContext context)
        {
            _context = context;
        }
        public void Add(RefreshToken refreshToken)
        {
            var entity = RefreshTokenMapper.ToEntity(refreshToken);
            _context.RefreshTokens.Add(entity);
        }

        public async Task<RefreshToken?> GetTokenByUserIdAsync(AccountId accoundId, CancellationToken cancellationToken)
        {
            var entity = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.AccountId == accoundId.Value && r.IsRevoked == false && r.ExpiresAt >= DateTime.UtcNow);
            if (entity is null) return null;
            return RefreshTokenMapper.ToDomain(entity);
        }

        public async Task RevokeAllActiveByAccoundIdAsync(AccountId accoundId, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens
                .Where(r => 
                    r.AccountId == accoundId.Value 
                    && r.IsRevoked == false 
                    && r.ExpiresAt > DateTime.UtcNow)
                .ExecuteUpdateAsync(s => 
                    s.SetProperty(r => r.IsRevoked, true),
                    cancellationToken);
        }
    }
}
