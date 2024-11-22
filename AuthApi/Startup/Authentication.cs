using AuthApi.DbContexts;
using AuthApi.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Startup
{
    public static class Authentication
    {
        public static void Register(WebApplicationBuilder builder)
        {
            /**
             * Configuration for the Microsoft Identity Framework
             */
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<MainDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            Shared.Startup.Authentication.Register(builder);
        }
    }
}
