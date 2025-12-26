namespace AuthorizationApi.Domain.Exceptions
{
    public class TokenAlreadyRevokedException : DomainException
    {
        public TokenAlreadyRevokedException()
        {
            
        }

        public TokenAlreadyRevokedException(string message) : base(message)
        {
        }

        public TokenAlreadyRevokedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
