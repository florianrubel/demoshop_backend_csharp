
namespace AuthApi.Models.Authentication
{
    public interface ICreateApiKey
    {
        List<string>? Roles { get; set; }
        string Scope { get; set; }
    }
}