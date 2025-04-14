using ContentCacheApi.Services;

namespace ContentCacheApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IContentCacheService, ContentCacheService>();
        }
    }
}
