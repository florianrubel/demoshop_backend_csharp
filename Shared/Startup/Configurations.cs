
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Authentication;

namespace Shared.Startup
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
