using Shared.Models;

namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public class ViewProductVariantStringProperty : UuidViewModel
    {
        public Guid? ProductVariantId { get; set; }

        public Guid? PropertyId { get; set; }

        public bool Value { get; set; } = false;
    }
}
