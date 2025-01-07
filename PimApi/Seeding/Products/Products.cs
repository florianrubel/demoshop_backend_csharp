using OpenAI.Chat;
using SharedProducts.Entities.Products;
using PimApi.Repositories.Products;
using Shared.Models.Api;
using System.Text.Json;

namespace PimApi.Seeding.Products
{
    class ChatGPTAnswer
    {
        public string product_name { get; set; }

        public Dictionary<string, string> description { get; set; }
    }
    public static class Products
    {
        private const string CACHE_FILENAME = "cache.products.json";

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeFile = true;
                var random = new Random();
                var apiKey = app.Configuration.GetSection("ChatGPT:ApiKey").Value;
                var repository = scope.ServiceProvider.GetService<IProductRepository<Product, SearchParameters>>();

                var products = new List<Product>();

                if (File.Exists(CACHE_FILENAME))
                {
                    var json = File.ReadAllText(CACHE_FILENAME);
                    products = JsonSerializer.Deserialize<List<Product>>(json);
                    await repository.CreateRange(products);
                    writeFile = false;
                } else
                {
                    ChatClient client = new(model: "gpt-3.5-turbo", apiKey: apiKey);

                    for (var i = 0; i < 100; i++)
                    {
                        var completionName = await client.CompleteChatAsync("give me a product name and description for a shirt as json with the keys product_name and description containing 10 to 20 sentenses without using the words T-shirt, shirt, top or tee. Translate the descriptions in the languages en-US, de-DE, it-IT, fr-FR, es-ES and and store it in description in the format { \"en-US\": \"localized description\" }. Return that answer as valid json.");
                        var gptJson = completionName.Value.Content[0].Text;
                        Console.WriteLine("#############################");

                        try
                        {
                            var answer = JsonSerializer.Deserialize<ChatGPTAnswer>(gptJson);
                            var product = new Product
                            {
                                Name = answer.product_name,
                                DescriptionLocalized = answer.description,
                                DefaultPriceInCents = random.Next(1000, 100000),
                            };
                            products.Add(await repository.Create(product));
                            Console.WriteLine($"{i}:{product.Name}");
                            Console.WriteLine($"{i}:{product.DescriptionLocalized}");
                        } catch
                        {
                            Console.WriteLine(gptJson);
                        }
                        Console.WriteLine("#############################");
                    }
                }

                if (writeFile)
                {
                    var wJson = JsonSerializer.Serialize(products);
                    File.WriteAllText(CACHE_FILENAME, wJson);
                }
            }
            Console.WriteLine("Products seeded");
        }
    }
}
