using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;
using AuthorizationApi.Application.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthorizationApi.Application.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthorizationApi.Application
{
    public static class Extensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<
                ICommandHandler<
                    RegisterCommand,
                    RegisterCommandResult>,
                RegisterUserHandler>();

            services.AddScoped<
                ICommandHandler<
                    SignInCommand,
                    SignInCommandResult>,
                SignInHandler>();
        }

        public static void AddAuthenticationLayer(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),

                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });
            services.AddAuthorization();
        }
    }
}
