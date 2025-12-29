using AuthorizationApi.Application.Interfaces;

namespace AuthorizationApi.Domain.ValueObjects
{
    public sealed record TokenHash(string Value)
    {
        public static TokenHash FromRaw(string rawToken, ITokenHashGenerator generator) => new(generator.GenerateHash(rawToken));
    }
}
