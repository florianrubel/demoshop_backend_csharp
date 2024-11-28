using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class PatchProductVariant
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public int? PriceInCents { get; set; }
    }
}
