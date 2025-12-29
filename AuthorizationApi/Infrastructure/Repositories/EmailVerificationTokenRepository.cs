using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Domain.Models;
using AuthorizationApi.Infrastructure.Mappers;

namespace AuthorizationApi.Infrastructure.Repositories
{
    public class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
    {
        private readonly AuthDbContext _context;
        public EmailVerificationTokenRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(EmailVerificationToken token, CancellationToken cancellationToken)
        {
            var entity = EmailVerificationTokenMapper.ToEntity(token);
            _context.EmailVerificationTokens.Add(entity);
            await Task.CompletedTask;
        }
    }
}
