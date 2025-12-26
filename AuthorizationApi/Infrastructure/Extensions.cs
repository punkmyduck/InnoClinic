using AuthorizationApi.Domain.Repositories;
using AuthorizationApi.Infrastructure.Repositories;

namespace AuthorizationApi.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
