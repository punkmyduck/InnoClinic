using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Exceptions;
using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Application.Options;
using AuthorizationApi.Application.Results;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;
using AuthorizationApi.Infrastructure.Security;

namespace AuthorizationApi.Application.Handlers
{
    public class SignInHandler : ICommandHandler<SignInCommand, SignInCommandResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtOptions _options;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHashGenerator _tokenHashGenerator;
        public SignInHandler(
            IAccountRepository accountRepository,
            IPasswordHasher passwordHasher,
            JwtOptions options,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork,
            ITokenHashGenerator tokenHashGenerator)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _options = options;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _tokenHashGenerator = tokenHashGenerator;
        }
        public async Task<SignInCommandResult> HandleAsync(SignInCommand command, CancellationToken cancellationToken)
        {
            Email email = new Email(command.Email);

            var account = await _accountRepository.GetByEmailAsync(email, cancellationToken);
            if (account == null) return new SignInCommandResult(false);

            if (!_passwordHasher.VerifyPassword(command.Password, account.PasswordHash.Value))
                return new SignInCommandResult(false);

            if (!account.IsEmailVerified)
                throw new EmailIsNotVerifiedException("Email requires verification before logging in.");

            DateTime now = DateTime.UtcNow;

            var claims = JwtClaimsFactory.Create(account, _options.Issuer);

            var accessToken = _jwtTokenGenerator.GenerateAccessToken(claims, now.AddMinutes(30));

            var generatedRefreshToken = _jwtTokenGenerator.GenerateRefreshToken(account.Id, now.AddDays(30));
            var refreshToken = RefreshToken.CreateToken(
                new RefreshTokenId(Guid.NewGuid()),
                account.Id,
                TokenHash.FromRaw(generatedRefreshToken.token, _tokenHashGenerator),
                generatedRefreshToken.expiresAt);

            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return new SignInCommandResult(true, accessToken.token, accessToken.expiresAt, generatedRefreshToken.token);
        }
    }
}
