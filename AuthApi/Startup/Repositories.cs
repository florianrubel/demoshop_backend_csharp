using AuthApi.Entities.Authentication;
using AuthApi.Repositories.Authentication;
using AuthApi.Repositories.Identity;
using Shared.Models.Api;

namespace AuthApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IApiKeyRepository<ApiKey, PaginationParameters>, ApiKeyRepository>();
        }
    }
}
