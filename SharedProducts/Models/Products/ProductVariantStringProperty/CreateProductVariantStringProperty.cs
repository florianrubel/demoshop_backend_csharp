using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public class CreateProductVariantStringProperty
    {
        [Required]
        public Guid? ProductVariantId { get; set; }

        [Required]
        public Guid? PropertyId { get; set; }

        public string Value { get; set; }
    }
}
