using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibUniversal.Startup
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
                // TODO: Make this configurable
                options.Authority = "https://localhost:7047";
                options.Audience = "https://localhost:5173";
                // Configure the Authority to the expected value for
                // the authentication provider. This ensures the token
                // is appropriately validated.
                //options.Authority = "Authority URL"; // TODO: Update URL

                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or
                // Server-Sent Events request comes in.

                // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
                // due to a limitation in Browser APIs. We restrict it to only calls to the
                // SignalR hub in this code.
                // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                // for more information about security considerations when using
                // the query string to transmit the access token.

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;

                        Console.WriteLine($"Incoming request to: {path}"); // Log request path
                        Console.WriteLine($"Extracted Access Token: {accessToken}"); // Log the token

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.ToString().StartsWith("/hubs/")))
                        {
                            Console.WriteLine("Hub request");
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Allow access of httpcontext outside of containers.
            builder.Services.AddHttpContextAccessor();
        }

        public static void PostBuild(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
