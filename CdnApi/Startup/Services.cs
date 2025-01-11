using CdnApi.Services;

namespace CdnApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IAwsS3Service, AwsS3Service>();
        }
    }
}
