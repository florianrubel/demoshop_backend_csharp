using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shared.Models.OpenApi;
using System.Reflection;

namespace Shared.Startup
{
    public static class OpenApi
    {
        public const string DEVELOPER_EMAIL = "florianrubel@display-awesome.com";
        public const string DEVELOPER_FULL_NAME = "Florian Rubel";

        public static void Register(WebApplicationBuilder builder, OpenApiMeta meta)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SupportNonNullableReferenceTypes();
                options.SwaggerDoc(meta.Version, new OpenApiInfo
                {
                    Version = meta.Version,
                    Title = meta.Name,
                    Description = meta.Description,
                    //TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = DEVELOPER_FULL_NAME,
                        Email = DEVELOPER_EMAIL,
                    },
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Example License",
                    //    Url = new Uri("https://example.com/license")
                    //}
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please provide a valid JWT token.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFilename = $"{meta.Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            }); //.AddSwaggerGenNewtonsoftSupport();
        }

        public static void PostBuild(WebApplication app, OpenApiMeta meta)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{meta.Version}/swagger.json", meta.Version);
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
