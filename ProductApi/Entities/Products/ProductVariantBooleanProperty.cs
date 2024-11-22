using ProductApi.Entities.Products.Properties;
using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Entities.Products
{
    public class ProductVariantBooleanProperty : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(BooleanPropertyId))]
        public Guid BooleanPropertyId { get; set; }
        public BooleanProperty MyProperty { get; set; }

        public bool Value { get; set; }
    }
}
