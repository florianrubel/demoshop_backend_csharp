using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shared.Models.Authentication;
using Shared.Models.Configuration;
using System.Text;
using System.Text.Json;

namespace SharedProducts.Services.ProductCache
{
    public class ProductCacheService : IProductCacheService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<ServicesConfiguration> _servicesConfiguration;

        public ProductCacheService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IOptions<ServicesConfiguration> servicesConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _servicesConfiguration = servicesConfiguration;
        }

        public async Task BuildCache()
        {
            await DoRequest("", new List<Guid>());
        }

        public async Task BuildByProductVariants(IEnumerable<Guid> ids)
        {
            await DoRequest("by-products", ids);
        }

        public async Task BuildByBooleanPropertys(IEnumerable<Guid> ids)
        {
            await DoRequest("by-product-variants", ids);
        }

        public async Task BuildByBooleanProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-boolean-properties", ids);
        }

        public async Task BuildByNumericProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-numeric-properties", ids);
        }

        public async Task BuildByStringProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-string-properties", ids);
        }

        public async Task BuildByProductVariantBooleanProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-product-variant-boolean-properties", ids);
        }

        public async Task BuildByProductVariantNumericProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-product-variant-numeric-properties", ids);
        }

        public async Task BuildByProductVariantStringProperties(IEnumerable<Guid> ids)
        {
            await DoRequest("by-product-variant-string-properties", ids);
        }

        private async Task DoRequest(string url, IEnumerable<Guid> ids)
        {
            var jwtToken = GetBearerToken();
            if (jwtToken == null)
            {
                jwtToken = await TrySignIn();
            }
            var requestUrl = $"{_servicesConfiguration.Value.ProductCacheApiUrl}/build-cache/{url}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                    var json = JsonSerializer.Serialize(ids);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                    response.EnsureSuccessStatusCode();

                    // Read response
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("REQUEST FAILED: " + requestUrl);
                    Console.WriteLine($"Product Cache Request error: {e.Message}");
                }
            }
        }
        private string? GetBearerToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.ToString().StartsWith("Bearer "))
            {
                return authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
            }
            return null;
        }
        private async Task<string?> TrySignIn()
        {
            var requestUrl = $"{_servicesConfiguration.Value.AuthApiUrl}/sign-in";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonSerializer.Serialize(new SignInUser
                    {
                        UserName = _servicesConfiguration.Value.SuperAdminUsername,
                        Password = _servicesConfiguration.Value.SuperAdminPassword,
                    });
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                    response.EnsureSuccessStatusCode();

                    // Read response
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var serializeOptions = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    };
                    var tokens = JsonSerializer.Deserialize<AuthenticationTokens>(responseBody, serializeOptions);
                    return tokens.AccessToken;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("REQUEST FAILED: " + requestUrl);
                    Console.WriteLine($"Product Cache Request error: {e.Message}");
                }
            }
            return null;
        }
    }
}
