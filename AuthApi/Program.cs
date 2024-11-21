using AuthApi.DbContexts;
using AuthApi.Entities.Identity;
using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var meta = new OpenApiMeta
{
    Name = "AuthApi",
    Version = "v1",
    Description = "Central api for authentication and managing users.",
    UriTerms = ""
};

AuthApi.Startup.Configurations.Register(builder);
Shared.Startup.Database<MainDbContext>.Register(builder, "AuthApi");
AuthApi.Startup.Services.Register(builder);
AuthApi.Startup.Repositories.Register(builder);
Shared.Startup.Authentication<MainDbContext, User, Role>.Register(builder);
Shared.Startup.Controllers.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.Database<MainDbContext>.PostBuild(app);
Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
Shared.Startup.Authentication<MainDbContext, User, Role>.PostBuild(app);

AuthApi.Seeding.Identity.Roles.Seed(app).Wait();
AuthApi.Seeding.Identity.Users.Seed(app).Wait();

app.Run();
