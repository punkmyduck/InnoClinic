namespace AuthorizationApi.Application.Exceptions
{
    public class EmailIsNotVerifiedException : ApplicationException
    {
        public EmailIsNotVerifiedException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
