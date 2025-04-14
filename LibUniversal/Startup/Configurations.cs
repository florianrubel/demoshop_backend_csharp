
using LibUniversal.Models.Authentication;
using LibUniversal.Models.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibUniversal.Startup
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
