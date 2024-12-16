using AuthApi.DbContexts;
using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for authentication and managing users.",
    UriTerms = ""
};

Shared.Startup.Configurations.Register(builder);
Shared.Startup.Database<MainDbContext>.Register(builder, assemblyName);
AuthApi.Startup.Services.Register(builder);
AuthApi.Startup.Repositories.Register(builder);
AuthApi.Startup.Authentication.Register(builder);
Shared.Startup.Controllers.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.Database<MainDbContext>.PostBuild(app);
Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
Shared.Startup.Authentication.PostBuild(app);

AuthApi.Seeding.Identity.Roles.Seed(app).Wait();
AuthApi.Seeding.Identity.Users.Seed(app).Wait();

app.Run();
