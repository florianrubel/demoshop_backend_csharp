using SharedProducts.DbContexts;
using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for caching property values",
    UriTerms = ""
};

Shared.Startup.Configurations.Register(builder);
Shared.Startup.Database<ReadOnlyDbContext>.Register(builder, assemblyName);
PropertyValueCacheApi.Startup.Services.Register(builder);
PropertyValueCacheApi.Startup.Repositories.Register(builder);
Shared.Startup.Authentication.Register(builder);
Shared.Startup.Controllers.Register(builder);
PropertyValueCacheApi.Startup.Hubs.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.Database<MainDbContext>.PostBuild(app);
Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
PropertyValueCacheApi.Startup.Hubs.PostBuild(app);
Shared.Startup.Authentication.PostBuild(app);

app.Run();