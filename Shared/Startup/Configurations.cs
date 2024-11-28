
using Castle.Core.Configuration;
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
                .Configure<IConfiguration>(builder.Configuration)
                .Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        }
    }
}
