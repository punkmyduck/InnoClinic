namespace AuthorizationApi.Application
{
    public static class Extensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<
                Abstractions.ICommandHandler<
                    Commands.RegisterCommand,
                    Results.RegisterCommandResult>,
                Handlers.RegisterUserHandler>();
        }
    }
}
