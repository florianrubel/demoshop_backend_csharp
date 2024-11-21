using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Shared.Startup
{
    public static class Database<DbContextType>
        where DbContextType : DbContext
    {
        public static void Register(WebApplicationBuilder builder, string dbName)
        {
            builder.Services.AddDbContext<DbContextType>(options =>
            {
                options
                    .UseInMemoryDatabase(databaseName: dbName)
                    // The following three options help with debugging, but should
                    // be changed or removed for production.
                    //.LogTo(Console.WriteLine, LogLevel.Information)
                    //.EnableSensitiveDataLogging()
                    //.EnableDetailedErrors()
                    .UseLazyLoadingProxies(true);
            });
        }
        public static void PostBuild(WebApplication app)
        {
            bool updateDatabaseEnabled = app.Configuration.GetValue<bool>("UpdateDatabase");
            if (updateDatabaseEnabled)
            {
                UpdateDatabase(app);
            }
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (DbContextType context = serviceScope.ServiceProvider.GetService<DbContextType>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
