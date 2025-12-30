using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void Add(RefreshToken refreshToken);
        Task RevokeAllActiveByAccoundIdAsync(AccountId accoundId, CancellationToken cancellationToken);
        Task<RefreshToken?> GetTokenByUserIdAsync(AccountId accoundId, CancellationToken cancellationToken);
    }
}
