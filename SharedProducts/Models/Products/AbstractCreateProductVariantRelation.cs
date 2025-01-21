using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products
{
    public abstract class AbstractCreateProductVariantRelation<ValueType>
    {
        [Required]
        public Guid? ProductVariantId { get; set; }

        [Required]
        public Guid? PropertyId { get; set; }

        public ValueType Value { get; set; }
    }
}
