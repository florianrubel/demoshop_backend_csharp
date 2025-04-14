using LibContent.Entities;
using LibUniversal.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace ContentApi.Services.ProductCache
{
    public class ContentCacheService : IContentCacheService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<ServicesConfiguration> _servicesConfiguration;

        public ContentCacheService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IOptions<ServicesConfiguration> servicesConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _servicesConfiguration = servicesConfiguration;
        }

        public async Task<Page?> GetContentCache(Guid id)
        {
            var jwtToken = GetBearerToken();
            if (jwtToken == null)
            {
                throw new UnauthorizedAccessException();
            }
            var requestUrl = $"{_servicesConfiguration.Value.ContentCacheApiUrl}/{id}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();

                    // Read response
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<Page>(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("REQUEST FAILED: " + requestUrl);
                    Console.WriteLine($"Content Cache Request error: {e.Message}");
                }
            }
            return null;
        }

        public async Task<Page> SetContentCache(Page page)
        {
            var jwtToken = GetBearerToken();
            if (jwtToken == null)
            {
                throw new UnauthorizedAccessException();
            }
            var requestUrl = $"{_servicesConfiguration.Value.ContentCacheApiUrl}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                    var json = JsonSerializer.Serialize(page);
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
                    Console.WriteLine($"Content Cache Request error: {e.Message}");
                }
            }
            return page;
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
    }
}
