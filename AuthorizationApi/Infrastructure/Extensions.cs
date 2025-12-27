using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Infrastructure.Repositories;
using AuthorizationApi.Infrastructure.Security;

namespace AuthorizationApi.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IPasswordPolicy, DefaultPasswordPolicy>();
        }
    }
}
