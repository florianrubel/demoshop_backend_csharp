using ProductApi.Entities.Products.Properties;
using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Entities.Products
{
    public class ProductVariantStringProperty : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(StringPropertyId))]
        public Guid StringPropertyId { get; set; }
        public StringProperty MyProperty { get; set; }

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Value { get; set; }
    }
}
