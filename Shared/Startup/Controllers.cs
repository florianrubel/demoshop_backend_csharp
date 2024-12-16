using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace Shared.Startup
{
    public static class Controllers
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
            .AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            })
            .AddJsonOptions(setupAction =>
            {
                setupAction.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                setupAction.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters();
        }

        public static void PostBuild(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllers();
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
    }
}
