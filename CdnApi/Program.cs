using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = typeof(Program).Assembly.GetName().Name;

var meta = new OpenApiMeta
{
    Name = assemblyName,
    Version = "v1",
    Description = "Central api for getting images from a cloud bucket.",
    UriTerms = ""
};

CdnApi.Startup.Configurations.Register(builder);
CdnApi.Startup.Services.Register(builder);
Shared.Startup.Authentication.Register(builder);
Shared.Startup.Controllers.Register(builder);
Shared.Startup.OpenApi.Register(builder, meta);
Shared.Startup.AutoMapping.Register(builder);

var app = builder.Build();

Shared.Startup.OpenApi.PostBuild(app, meta);
Shared.Startup.Cors.PostBuild(app);
Shared.Startup.Controllers.PostBuild(app);
Shared.Startup.Authentication.PostBuild(app);

app.Run();