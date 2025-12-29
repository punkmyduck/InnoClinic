using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Infrastructure.Repositories;
using AuthorizationApi.Infrastructure.Security;
using AuthorizationApi.Infrastructure.UnitOfWork;

namespace AuthorizationApi.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IPasswordPolicy, DefaultPasswordPolicy>();
            services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();
            services.AddTransient<ITokenHashGenerator, Sha256TokenHashGenerator>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        }
    }
}
