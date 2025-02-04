using PropertyValueCacheApi.Hubs;

namespace PropertyValueCacheApi.Startup
{
    public static class Hubs
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IPropertyValueCacheHub, PropertyValueCacheHub>();


            builder.Services.AddSignalR();
        }
        public static void PostBuild(WebApplication app)
        {
            app.MapHub<PropertyValueCacheHub>("hubs/property-value-cache");
        }
    }
}
