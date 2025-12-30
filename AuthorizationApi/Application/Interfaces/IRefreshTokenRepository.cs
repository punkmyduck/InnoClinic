using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;

namespace AuthorizationApi.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task<RefreshToken?> GetTokenByUserId(AccountId accoundId);
    }
}
