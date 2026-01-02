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

        public async Task RevokeAllActiveByAccountIdAsync(AccountId accoundId, CancellationToken cancellationToken)
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

        public async Task<RefreshToken?> GetTokenByHash(TokenHash tokenHash, CancellationToken cancellationToken)
        {
            var token = await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.TokenHash == tokenHash.Value, cancellationToken);

            if (token is null) return null;

            return RefreshTokenMapper.ToDomain(token);
        }

        public void Update(RefreshToken token)
        {
            _context.RefreshTokens
                .Where(r => r.Id == token.Id.Value)
                .ExecuteUpdate(s =>
                    s.SetProperty(r => r.IsRevoked, token.IsRevoked));
        }
    }
}
