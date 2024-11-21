using AuthApi.DbContexts;
using AuthApi.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            /**
             * Configuration for the Jwt Authentication
             */
            string issuer = builder.Configuration["JwtSettings:Issuer"] ?? throw new NullReferenceException("Configuration:JwtSettings:Issuer");
            string audience = builder.Configuration["JwtSettings:Audience"] ?? throw new NullReferenceException("Configuration:JwtSettings:Audience");
            string key = builder.Configuration["JwtSettings:Key"] ?? throw new NullReferenceException("Configuration:JwtSettings:Key");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };

            builder.Services.AddSingleton(tokenValidationParameters);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

            /**
             * Configuration for the usage of roles, claims and policies
             */
            builder.Services.AddAuthorization(options =>
            {
                // setup policies here
            });
        }

        public static void PostBuild(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
