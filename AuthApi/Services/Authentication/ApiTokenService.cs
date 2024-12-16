using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthApi.Services.Authentication
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public ApiTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /**
         * Generates an api token for a specific scope (api)
         */
        public string GenerateToken(string scope, IEnumerable<string> roles)
        {
            var tokenId = Guid.NewGuid().ToString();
            var userClaims = new List<Claim>
            {
                new(Shared.Constants.Authentication.CLAIM_SCOPE, scope)
            };
            foreach (var role in roles)
            {
                userClaims.Add(new(ClaimTypes.Role, role));
            }
            var token = GenerateToken(
                tokenId,
                scope,
                DateTime.UtcNow.AddMonths(_jwtSettings.Value.ApiTokenTTL | 1),
                userClaims
            );
            return token;
        }

        private string GenerateToken(
            string identifier,
            string subject,
            DateTime expires,
            IEnumerable<Claim>? additionalClaims = null
        )
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Value.Key);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, identifier),
                new(JwtRegisteredClaimNames.Sub, subject),
            };
            if (additionalClaims != null) claims.AddRange(additionalClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = _jwtSettings.Value.Issuer,
                Audience = _jwtSettings.Value.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
