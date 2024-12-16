using Shared.Models.OpenApi;
using StockApi.DbContexts;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for managing stock of product variants.",
    UriTerms = ""
};

Shared.Startup.Configurations.Register(builder);
Shared.Startup.Database<MainDbContext>.Register(builder, assemblyName);
StockApi.Startup.Services.Register(builder);
StockApi.Startup.Repositories.Register(builder);
Shared.Startup.Authentication.Register(builder);
Shared.Startup.Controllers.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.Database<MainDbContext>.PostBuild(app);
Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
Shared.Startup.Authentication.PostBuild(app);

app.Run();