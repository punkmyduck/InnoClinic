namespace AuthorizationApi.Application.Exceptions
{
    public class PasswordDoesNotMeetComplexityException : ApplicationException
    {
        public PasswordDoesNotMeetComplexityException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
