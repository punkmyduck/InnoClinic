namespace AuthorizationApi.Application.Exceptions
{
    public class EmailAlreadyExistsException : ApplicationException
    {
        public EmailAlreadyExistsException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
