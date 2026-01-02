namespace AuthorizationApi.Application.Exceptions
{
    public class RefreshTokenIsRevokedException : ApplicationException
    {
        public RefreshTokenIsRevokedException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
