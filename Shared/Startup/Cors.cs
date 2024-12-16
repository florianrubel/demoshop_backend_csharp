using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Shared.Startup
{
    public static class Cors
    {
        public static void PostBuild(WebApplication app)
        {
            string? pwaUri = app.Configuration.GetValue<string>("Pwa:Uri") ?? throw new NullReferenceException("Pwa:Uri");
            app.UseCors(
                options =>
                {
                    options.WithOrigins(pwaUri != null ? pwaUri : app.Urls.First());
                    options.WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS");
                    options.AllowAnyHeader();
                    options.AllowCredentials();
                    options.SetIsOriginAllowed((host) => true);
                    options.WithExposedHeaders(new string[]
                    {
                        "Pagination.TotalCount",
                        "Pagination.PageSize",
                        "Pagination.Page",
                        "Pagination.TotalPages"
                    });
                });
        }
    }
}
