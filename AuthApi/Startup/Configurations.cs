using AuthApi.Models.Authentication;

namespace AuthApi.Startup
{
    public static class Configurations
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        }
    }
}
