namespace AuthorizationApi.Domain.Exceptions
{
    public class PasswordResetTokenAlreadyUsedException : DomainException
    {
        public PasswordResetTokenAlreadyUsedException() { }

        public PasswordResetTokenAlreadyUsedException(string message) : base(message) { }

        public PasswordResetTokenAlreadyUsedException(string message, Exception inner) : base(message, inner) { }
    }
}
