using SharedProducts.DbContexts;
using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for caching and provide products.",
    UriTerms = ""
};

ProductCacheApi.Startup.Configurations.Register(builder);
Shared.Startup.Database<ReadOnlyDbContext>.Register(builder, assemblyName);
ProductCacheApi.Startup.Repositories.Register(builder);
ProductCacheApi.Startup.Caches.Register(builder);
Shared.Startup.Authentication.Register(builder);
Shared.Startup.Controllers.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.Database<ReadOnlyDbContext>.PostBuild(app);
Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
Shared.Startup.Authentication.PostBuild(app);

ProductCacheApi.Seeding.ProductCache.Seed(app).Wait();

var lifetime = app.Lifetime;
lifetime.ApplicationStopping.Register(() =>
{
    ProductCacheApi.Seeding.ProductCache.UnSeed(app).Wait();
});

app.Run();