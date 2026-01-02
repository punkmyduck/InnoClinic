using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Exceptions;
using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Application.Options;
using AuthorizationApi.Application.Results;
using AuthorizationApi.Application.Secutiry;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace AuthorizationApi.Application.Handlers
{
    public class RefreshHandler : ICommandHandler<RefreshCommand, RefreshCommandResult>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenHashGenerator _tokenHashGenerator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly JwtOptions _options;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public RefreshHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IAccountRepository accountRepository,
            ITokenHashGenerator tokenHashGenerator,
            IJwtTokenGenerator jwtTokenGenerator,
            IOptions<JwtOptions> options,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _accountRepository = accountRepository;
            _tokenHashGenerator = tokenHashGenerator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _options = options.Value;
            _refreshTokenGenerator = refreshTokenGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<RefreshCommandResult> HandleAsync(RefreshCommand command, CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;
            TokenHash tokenHash = TokenHash.FromRaw(command.RefreshToken, _tokenHashGenerator);

            await _unitOfWork.BeginAsync(cancellationToken);
            try
            {
                //1. Load refresh token from database
                RefreshToken? refreshToken = 
                    await _refreshTokenRepository.GetTokenByHash(tokenHash, cancellationToken);

                if (refreshToken == null)
                    throw new RefreshTokenNotFoundException("Refresh token not found.");

                //2. Reuse detection -> security incident
                if (refreshToken.IsRevoked)
                {
                    await _refreshTokenRepository
                        .RevokeAllActiveByAccountIdAsync(
                            refreshToken.AccountId, 
                            cancellationToken);

                    await _unitOfWork.CommitAsync(cancellationToken);

                    throw new RefreshTokenIsRevokedException(
                        "This refresh token was revoked. Reuse detected. All sessions revoked.");
                }

                //3. Expiration check
                if (refreshToken.IsExpired(now))
                    throw new RefreshTokenIsExpiredException("Refresh token is expired.");

                //4. Load associated account
                Account? account = await _accountRepository
                    .GetByAccountIdAsync(refreshToken.AccountId, cancellationToken);

                if (account is null)
                    throw new AccountNotFoundException("Account associated with refresh token not found.");

                //5. RefreshTokens rotation
                refreshToken.Revoke();
                _refreshTokenRepository.Update(refreshToken);

                var generatedRefreshToken = 
                    _refreshTokenGenerator.GenerateRefreshToken(
                        account.Id, 
                        now.AddDays(30));

                var newRefreshToken = RefreshToken.CreateToken(
                    new RefreshTokenId(Guid.NewGuid()),
                    account.Id,
                    TokenHash.FromRaw(
                        generatedRefreshToken.token,
                        _tokenHashGenerator),
                    generatedRefreshToken.expiresAt);

                _refreshTokenRepository.Add(newRefreshToken);

                //6. Generate new JWT access token
                var claims = JwtClaimsFactory.Create(account, _options.Issuer);
                var accessToken = _jwtTokenGenerator.GenerateAccessToken(claims, now.AddMinutes(_options.LifeTimeInMinutes));


                await _unitOfWork.CommitAsync(cancellationToken);

                return new RefreshCommandResult(accessToken.token, accessToken.expiresAt, generatedRefreshToken.token);
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
