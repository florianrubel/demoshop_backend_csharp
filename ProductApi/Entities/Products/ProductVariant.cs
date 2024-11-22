using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Entities.Products
{
    public class ProductVariant : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public int PriceInCents { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
