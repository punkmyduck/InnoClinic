using AuthorizationApi.Application.Abstractions;
using AuthorizationApi.Application.Commands;
using AuthorizationApi.Application.Results;

namespace AuthorizationApi.Application.Handlers
{
    public class RegisterUserHandler : ICommandHandler<RegisterCommand, RegisterCommandResult>
    {
        public RegisterUserHandler()
        {
            
        }
        public Task<RegisterCommandResult> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
        {
            //TODO: реализовать регистрацию пользователя
            //проверить существование почты
            //проверить надежность пароля
            //создать хеш пароля
            //создать гуид пользователя
            //создать сущность пользователя
            //создать верификационный токен почты
            //сохранить пользователя в бд
            //сохранить токен в бд
            //закоммитить работу
            //вернуть результат с гуидом пользователя
            throw new NotImplementedException();
        }
    }
}