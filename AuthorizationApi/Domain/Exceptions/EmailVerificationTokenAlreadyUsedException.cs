namespace AuthorizationApi.Domain.Exceptions
{
    public class EmailVerificationTokenAlreadyUsedException : DomainException
    {
        public EmailVerificationTokenAlreadyUsedException()
        {

        }

        public EmailVerificationTokenAlreadyUsedException(string message) : base(message)
        {
        }

        public EmailVerificationTokenAlreadyUsedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
