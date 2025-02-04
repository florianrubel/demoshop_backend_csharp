using PimApi.Models;

namespace PimApi.Startup
{
    public static class Configurations
    {
        public static void Register(WebApplicationBuilder builder)
        {
            Shared.Startup.Configurations.Register(builder);
            builder.Services
                .Configure<AlgoliaSettings>(builder.Configuration.GetSection("AlgoliaSettings"));
        }
    }
}
