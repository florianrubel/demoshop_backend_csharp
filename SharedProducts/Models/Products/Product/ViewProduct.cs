using Shared.Entities;

namespace SharedProducts.Models.Products.Product
{
    public class ViewProduct : UuidBaseEntity
    {
        public string Name { get; set; }

        public Dictionary<string, string> DescriptionLocalized { get; set; }

        public int DefaultPriceInCents { get; set; }
    }
}
