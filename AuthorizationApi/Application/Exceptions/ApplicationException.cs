namespace AuthorizationApi.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string message, Exception? inner = null) : base(message, inner)
        {
            
        }
    }
}
