using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken);
        Task RevokeAllActiveByAccountIdAsync(AccountId accoundId, CancellationToken cancellationToken);
        void Update(RefreshToken token);
        Task<RefreshToken?> GetTokenByHash(TokenHash tokenHash, CancellationToken cancellationToken);
    }
}
