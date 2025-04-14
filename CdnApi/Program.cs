using LibUniversal.Models.OpenApi;

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
LibUniversal.Startup.Authentication.Register(builder);
LibUniversal.Startup.Controllers.Register(builder);
LibUniversal.Startup.OpenApi.Register(builder, meta);
LibUniversal.Startup.AutoMapping.Register(builder);

var app = builder.Build();

LibUniversal.Startup.OpenApi.PostBuild(app, meta);
LibUniversal.Startup.Cors.PostBuild(app);
LibUniversal.Startup.Controllers.PostBuild(app);
LibUniversal.Startup.Authentication.PostBuild(app);

app.Run();