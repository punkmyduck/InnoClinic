namespace AuthorizationApi.Application.Interfaces
{
    public interface IPasswordPolicy
    {
        public bool IsValid(string password);
    }
}
