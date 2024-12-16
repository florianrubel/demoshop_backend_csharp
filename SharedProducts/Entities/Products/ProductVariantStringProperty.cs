using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedProducts.Entities.Products
{
    public class ProductVariantStringProperty : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Guid PropertyId { get; set; }
        public virtual StringProperty Property { get; set; }

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Value { get; set; }
    }
}
