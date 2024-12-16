using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariantBooleanProperty
{
    public class CreateProductVariantBooleanProperty
    {
        [Required]
        public Guid? ProductVariantId { get; set; }

        [Required]
        public Guid? PropertyId { get; set; }

        public bool Value { get; set; } = false;
    }
}
