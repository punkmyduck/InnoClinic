namespace AuthorizationApi.Application.Interfaces
{
    public interface ITokenHashGenerator
    {
        public string GenerateHash(string rawToken);
        public bool VerifyToken(string rawToken, string tokenHash);
    }
}
