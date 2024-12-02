using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Shared.Models.OpenApi;

namespace Shared.Startup
{
    public static class OpenApi
    {
        public const string DEVELOPER_EMAIL = "florianrubel@display-awesome.com";
        public const string DEVELOPER_FULL_NAME = "Florian Rubel";

        public static void Register(WebApplicationBuilder builder, OpenApiMeta meta)
        {
            builder.Services.AddOpenApi(options =>
            {
            });
        }

        public static void PostBuild(WebApplication app, OpenApiMeta meta)
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}
