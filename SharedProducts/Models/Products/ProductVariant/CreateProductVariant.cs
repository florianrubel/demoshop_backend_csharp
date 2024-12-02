using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class CreateProductVariant
    {
        public int PriceInCents { get; set; }

        [Required]
        public Guid? ProductId { get; set; }
    }
}
