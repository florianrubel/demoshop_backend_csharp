using Shared.Models;

namespace SharedProducts.Models.Products
{
    public abstract class AbstractViewProductVariantRelation<ValueType> : UuidViewModel
    {
        public Guid? ProductVariantId { get; set; }

        public Guid? PropertyId { get; set; }

        public ValueType Value { get; set; }
    }
}
