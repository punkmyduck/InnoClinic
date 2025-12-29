namespace AuthorizationApi.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool VerifyPassword(string password, string hash);
    }
}
