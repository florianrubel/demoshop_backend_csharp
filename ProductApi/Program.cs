using ProductApi.DbContexts;
using Shared.Models.OpenApi;

var builder = WebApplication.CreateBuilder(args);

var meta = new OpenApiMeta
{
    Name = "ProductApi",
    Version = "v1",
    Description = "Central api for managing products.",
    UriTerms = ""
};

Shared.Startup.Configurations.Register(builder);
Shared.Startup.Database<MainDbContext>.Register(builder, "ProductApi");
//ProductApi.Startup.Services.Register(builder);
ProductApi.Startup.Repositories.Register(builder);
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

ProductApi.Seeding.Products.Properties.BooleanProperties.Seed(app).Wait();
ProductApi.Seeding.Products.Properties.NumericProperties.Seed(app).Wait();
ProductApi.Seeding.Products.Properties.StringProperties.Seed(app).Wait();

app.Run();