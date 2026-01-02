namespace AuthorizationApi.Application.Exceptions
{
    public class RefreshTokenIsExpiredException : ApplicationException
    {
        public RefreshTokenIsExpiredException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
