namespace AuthApi.Models.Authentication.ApiKey
{
    public interface ICheckApiKeyRequest
    {
        string ApiKey { get; set; }
    }
}