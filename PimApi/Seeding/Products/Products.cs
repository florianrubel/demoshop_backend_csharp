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

        public string description { get; set; }
    }
    public static class Products
    {
        private const string CACHE_FILENAME = "cache.products.json";

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var random = new Random();
                var apiKey = app.Configuration.GetSection("ChatGPT:ApiKey").Value;
                var repository = scope.ServiceProvider.GetService<IProductRepository<Product, ISearchParameters>>();

                var products = new List<Product>();

                if (File.Exists(CACHE_FILENAME))
                {
                    var json = File.ReadAllText(CACHE_FILENAME);
                    products = JsonSerializer.Deserialize<List<Product>>(json);
                } else
                {
                    ChatClient client = new(model: "gpt-3.5-turbo", apiKey: apiKey);

                    for (var i = 0; i < 100; i++)
                    {
                        var completionName = await client.CompleteChatAsync("give me a product name and description for a shirt as json with the keys product_name and description without using the words T-shirt, shirt, top or tee");
                        var gptJson = completionName.Value.Content[0].Text;
                        var answer = JsonSerializer.Deserialize<ChatGPTAnswer>(gptJson);
                        var product = new Product
                        {
                            Name = answer.product_name, //.Replace(" T-Shirt", "").Replace(" t-Shirt", "").Replace(" T-shirt", "").Replace(" Tee", "").Replace(" Shirt", ""),
                            Description = answer.description,
                            DefaultPriceInCents = random.Next(1000, 100000),
                            ListPicture = "https://picsum.photos/300/300",
                            Pictures = new List<string>
                        {
                            "https://picsum.photos/500/500",
                            "https://picsum.photos/500/500",
                            "https://picsum.photos/500/500",
                            "https://picsum.photos/500/500",
                            "https://picsum.photos/500/500"
                        }
                        };
                        products.Add(product);
                        Console.WriteLine("#############################");
                        Console.WriteLine($"{i}:{product.Name}");
                        Console.WriteLine($"{i}:{product.Description}");
                        Console.WriteLine("#############################");
                    }
                }

                products = (await repository.CreateRange(products)).ToList();

                var wJson = JsonSerializer.Serialize(products);
                File.WriteAllText(CACHE_FILENAME, wJson);
            }
            Console.WriteLine("Products seeded");
        }
    }
}
