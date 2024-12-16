using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariantNumericProperty
{
    public class CreateProductVariantNumericProperty
    {
        [Required]
        public Guid? ProductVariantId { get; set; }

        [Required]
        public Guid? PropertyId { get; set; }

        public double Value { get; set; }
    }
}
