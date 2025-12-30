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
        public void Add(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            var entity = RefreshTokenMapper.ToEntity(refreshToken);
            _context.RefreshTokens.Add(entity);
        }

        public async Task<RefreshToken?> GetTokenByUserIdAsync(AccountId accoundId)
        {
            var entity = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.AccountId == accoundId.Value && r.IsRevoked == false && r.ExpiresAt >= DateTime.UtcNow);
            if (entity is null) return null;
            return RefreshTokenMapper.ToDomain(entity);
        }
    }
}
