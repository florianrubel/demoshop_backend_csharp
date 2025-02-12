
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Authentication;
using Shared.Models.Configuration;

namespace Shared.Startup
{
    public static class Configurations
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .Configure<IConfiguration>(builder.Configuration)
                .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"))
                .Configure<ServicesConfiguration>(builder.Configuration.GetSection("Services"));
        }
    }
}
