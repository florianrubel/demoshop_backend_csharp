using AuthApi.Repositories.Identity;

namespace AuthApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IUserRepository, UserRepository>();
        }
    }
}
