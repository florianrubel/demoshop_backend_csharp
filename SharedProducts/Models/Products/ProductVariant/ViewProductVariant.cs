using Shared.Entities;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ViewProductVariant : UuidBaseEntity
    {
        public int PriceInCents { get; set; }

        public Guid ProductId { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; }
    }
}
