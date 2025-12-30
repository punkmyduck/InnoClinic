using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Exceptions;
using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Application.Results;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Domain.ValueObjects;
using System.Security.Cryptography;

namespace AuthorizationApi.Application.Handlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterCommand, RegisterCommandResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
        private readonly IPasswordPolicy _passwordPolicy;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHashGenerator _tokenHashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserHandler(
            IAccountRepository accountRepository,
            IEmailVerificationTokenRepository emailVerificationTokenRepository,
            IPasswordPolicy passwordPolicy,
            IPasswordHasher passwordHasher,
            ITokenHashGenerator tokenHashGenerator,
            IUnitOfWork unitOfWork
            )
        {
            _accountRepository = accountRepository;
            _emailVerificationTokenRepository = emailVerificationTokenRepository;
            _passwordPolicy = passwordPolicy;
            _passwordHasher = passwordHasher;
            _tokenHashGenerator = tokenHashGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterCommandResult> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
        {
            Email email = new Email(command.Email);

            if (await _accountRepository.ExistsByEmailAsync(email, cancellationToken))
                throw new EmailAlreadyExistsException("This email already taken.");

            if (!_passwordPolicy.IsValid(command.Password))
                throw new PasswordDoesNotMeetComplexityException("Password does not meet complexity requirements.");

            string hashedPassword = _passwordHasher.GenerateHash(command.Password);

            Guid userGuid = Guid.NewGuid();
            DateTime now = DateTime.UtcNow;

            Account newAccount = Account.Create(
                new AccountId(userGuid),
                email,
                new PasswordHash(hashedPassword),
                command.PhoneNumber is null ? null : new PhoneNumber(command.PhoneNumber),
                now,
                new AccountId(Guid.Empty));

            string rawCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

            EmailVerificationToken emailVerificationToken = EmailVerificationToken.CreateToken(
                new EmailVerificationTokenId(Guid.NewGuid()),
                newAccount.Id,
                TokenHash.FromRaw(rawCode, _tokenHashGenerator),
                now.AddMinutes(15));

            //Добавить вызов метода отдельного класса для отправки кода на почту

            _accountRepository.Add(newAccount);
            _emailVerificationTokenRepository.Add(emailVerificationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new RegisterCommandResult(newAccount.Id.Value, true);
        }
    }
}