using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Entities.Products
{
    public class ProductVariant : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public int PriceInCents { get; set; }
    }
}
