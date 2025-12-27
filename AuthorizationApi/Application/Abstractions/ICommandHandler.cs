namespace AuthorizationApi.Application.Abstractions
{
    public interface ICommandHandler<C, R> 
        where C : ICommand 
        where R : ICommandResult
    {
        public Task<R> HandleAsync(C command, CancellationToken cancellationToken);
    }
}
