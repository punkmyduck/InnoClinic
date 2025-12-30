using AuthorizationApi.Application.Interfaces;
using AuthorizationApi.Application.Options;
using AuthorizationApi.Application.ValueObjects;
using AuthorizationApi.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthorizationApi.Infrastructure.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSecurityTokenHandler _handler = new();
        private readonly JwtOptions _options;

        public JwtTokenGenerator(JwtOptions options)
        {
            _options = options;
        }
        public JwtTokenResult GenerateAccessToken(IReadOnlyCollection<Claim> claims, DateTime expiresAt)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtTokenResult(
                _handler.WriteToken(token),
                expiresAt);
        }

        public JwtTokenResult GenerateRefreshToken(AccountId accountId, DateTime expiresAt)
        {
            var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return new JwtTokenResult(raw, expiresAt);
        }
    }
}
