using AuthApi.Services.Authentication;

namespace AuthApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<IApiTokenService, ApiTokenService>();
        }
    }
}
