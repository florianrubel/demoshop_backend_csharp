using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class CreateProductVariant
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public int PriceInCents { get; set; }

        [Required]
        public Guid? ProductId { get; set; }
    }
}
