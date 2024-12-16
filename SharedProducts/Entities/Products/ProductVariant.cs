using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedProducts.Entities.Products
{
    public class ProductVariant : UuidBaseEntity
    {
        public int PriceInCents { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; } = new List<string>();
    }
}
