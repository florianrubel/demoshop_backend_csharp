
namespace AuthApi.Services.Authentication
{
    public interface IApiTokenService
    {
        string GenerateToken(string scope, IEnumerable<string> roles);
    }
}