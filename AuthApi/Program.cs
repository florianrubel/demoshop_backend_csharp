using AuthApi.DbContexts;
using LibUniversal.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for authentication and managing users.",
    UriTerms = ""
};

LibUniversal.Startup.Configurations.Register(builder);
LibDb.Startup.Database<MainDbContext>.Register(builder, assemblyName);
AuthApi.Startup.Services.Register(builder);
AuthApi.Startup.Repositories.Register(builder);
AuthApi.Startup.Authentication.Register(builder);
LibUniversal.Startup.Controllers.Register(builder);
LibUniversal.Startup.OpenApi.Register(builder, meta);
LibUniversal.Startup.AutoMapping.Register(builder);

var app = builder.Build();

LibDb.Startup.Database<MainDbContext>.PostBuild(app);
LibUniversal.Startup.OpenApi.PostBuild(app, meta);
LibUniversal.Startup.Cors.PostBuild(app);
LibUniversal.Startup.Controllers.PostBuild(app);
LibUniversal.Startup.Authentication.PostBuild(app);

AuthApi.Seeding.Identity.Roles.Seed(app).Wait();
AuthApi.Seeding.Identity.Users.Seed(app).Wait();

app.Run();
