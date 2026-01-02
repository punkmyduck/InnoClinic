namespace AuthorizationApi.Application.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SigningKey { get; set; } = null!;
        public int LifeTimeInMinutes { get; set; }
    }
}
