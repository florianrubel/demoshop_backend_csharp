using AuthApi.Entities.Identity;
using Shared.Models.Authentication;

namespace AuthApi.Services.Authentication
{
    public interface IJwtService
    {
        AuthenticationTokens GenerateTokenPair(User user, IEnumerable<string> roles, string ipAddress);
        string GetUserIdFromToken(string token);
        bool ValidateTokenPairForRefresh(AuthenticationTokens tokens, string ipAddress);
    }
}