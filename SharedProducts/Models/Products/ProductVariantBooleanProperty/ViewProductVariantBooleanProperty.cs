using Shared.Models;

namespace SharedProducts.Models.Products.ProductVariantBooleanProperty
{
    public class ViewProductVariantBooleanProperty : UuidViewModel
    {
        public Guid? ProductVariantId { get; set; }

        public Guid? PropertyId { get; set; }

        public bool Value { get; set; } = false;
    }
}
