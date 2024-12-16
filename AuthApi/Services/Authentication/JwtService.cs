using AuthApi.Entities.Identity;
using AuthApi.Models.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Authentication;
using Shared.StaticServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthApi.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly TokenValidationParameters _tokenValidationParametersWithoutExpire;

        public JwtService(IOptions<JwtSettings> jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _tokenValidationParametersWithoutExpire = _tokenValidationParameters.Clone();
            _tokenValidationParametersWithoutExpire.RequireExpirationTime = false;
        }

        /**
         * Generates a token pair where the access token generates a new
         * Guid as an identifier and the refresh token gets this Guid
         * as the subject. So later we can verify that connection.
         */
        public AuthenticationTokens GenerateTokenPair(User user, IEnumerable<string> roles, string ipAddress)
        {
            var accessTokenId = Guid.NewGuid().ToString();
            var userClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName)
            };
            foreach (var role in roles)
            {
                userClaims.Add(new(ClaimTypes.Role, role));
            }
            var accessToken = GenerateToken(
                ipAddress,
                accessTokenId,
                user.Id,
                DateTime.UtcNow.AddMinutes(_jwtSettings.Value.BearerTTL | 30),
                userClaims
            );
            var refreshToken = GenerateToken(
                ipAddress,
                Guid.NewGuid().ToString(),
                accessTokenId,
                DateTime.UtcNow.AddDays(_jwtSettings.Value.RefreshTTL | 1)
            );
            return new AuthenticationTokens
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public string? GetUserIdFromToken(string token)
        {
            var securityToken = GetJwtSecurityTokenFromTokenOrDefault(token, _tokenValidationParametersWithoutExpire);

            if (securityToken == null) return null;

            var claim = securityToken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault();

            if (claim == null) return null;

            return claim.Value;
        }

        public bool ValidateTokenPairForRefresh(AuthenticationTokens tokens, string ipAddress)
        {
            // We can ignore the expiration of the access token
            // because the expiration time of the refresh token matters.
            var accessSecurityToken = GetJwtSecurityTokenFromTokenOrDefault(tokens.AccessToken, _tokenValidationParametersWithoutExpire);
            var refreshSecurityToken = GetJwtSecurityTokenFromTokenOrDefault(tokens.RefreshToken, _tokenValidationParameters);

            // If one is null, the token was invalid.
            if (accessSecurityToken == null || refreshSecurityToken == null) return false;

            var accessIdentifier = accessSecurityToken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Jti).FirstOrDefault();
            var refreshSubject = refreshSecurityToken.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault();

            // If one of the claims is missing, the token was invalid.
            if (accessIdentifier == null || refreshSubject == null) return false;

            // Only if the identifier of the access token matches with the
            // subject of the refresh token, both are actually valid.
            // This method prevents us from storing the refresh tokens
            // in the database.
            if (accessIdentifier.Value != refreshSubject.Value) return false;

            var accessDeviceId = accessSecurityToken.Claims.Where(c => c.Type == Shared.Constants.Authentication.CLAIM_DEVICE_ID).FirstOrDefault();
            var refreshDeviceId = refreshSecurityToken.Claims.Where(c => c.Type == Shared.Constants.Authentication.CLAIM_DEVICE_ID).FirstOrDefault();

            // If one of the claims is missing, the token was invalid.
            if (accessDeviceId == null || refreshDeviceId == null) return false;

            // If the ip address hashes mismatch, the token was invalid.
            if (accessDeviceId.Value == refreshDeviceId.Value && TextService.GetHash(ipAddress) == accessDeviceId.Value) return true;

            return false;
        }

        private JwtSecurityToken? GetJwtSecurityTokenFromTokenOrDefault(string token, TokenValidationParameters tokenValidationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return tokenHandler.ReadJwtToken(token);
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GenerateToken(
            string ipAddress,
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
                new(Shared.Constants.Authentication.CLAIM_DEVICE_ID, TextService.GetHash(ipAddress))
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
