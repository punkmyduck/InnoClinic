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
            services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<IPasswordPolicy, DefaultPasswordPolicy>();
            services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
            services.AddSingleton<ITokenHashGenerator, Sha256TokenHashGenerator>();
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
