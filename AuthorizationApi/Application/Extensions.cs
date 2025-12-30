using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;
using AuthorizationApi.Application.Handlers;

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
    }
}
