using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.Startup
{
    public static class Authentication
    {
        public static void Register(WebApplicationBuilder builder)
        {
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
