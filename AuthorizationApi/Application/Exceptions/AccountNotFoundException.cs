namespace AuthorizationApi.Application.Exceptions
{
    public class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
