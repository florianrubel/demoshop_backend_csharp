using ProductApi.Entities.Products.Properties;
using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Entities.Products
{
    public class ProductVariantNumericProperty : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(NumericPropertyId))]
        public Guid NumericPropertyId { get; set; }
        public NumericProperty MyProperty { get; set; }

        public double Value { get; set; }
    }
}
