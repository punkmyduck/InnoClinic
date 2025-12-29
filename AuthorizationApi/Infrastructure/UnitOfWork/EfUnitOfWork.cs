using AuthorizationApi.Application.Interfaces;

namespace AuthorizationApi.Infrastructure.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;
        public EfUnitOfWork(AuthDbContext context)
        {
            _context = context;
        }
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
