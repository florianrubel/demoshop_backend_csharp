using Shared.Entities;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ViewProductVariant : UuidBaseEntity
    {
        public string? Name { get; set; }

        public int PriceInCents { get; set; }

        public Guid ProductId { get; set; }
    }
}
