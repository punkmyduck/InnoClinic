using Microsoft.EntityFrameworkCore;

namespace AuthorizationApi.Infrastructure
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }
    }
}
