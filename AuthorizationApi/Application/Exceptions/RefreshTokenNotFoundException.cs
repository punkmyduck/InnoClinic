namespace AuthorizationApi.Application.Exceptions
{
    public class RefreshTokenNotFoundException : ApplicationException
    {
        public RefreshTokenNotFoundException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
