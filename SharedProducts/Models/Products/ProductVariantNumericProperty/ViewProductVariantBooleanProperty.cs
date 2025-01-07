using Shared.Models;

namespace SharedProducts.Models.Products.ProductVariantNumericProperty
{
    public class ViewProductVariantNumericProperty : UuidViewModel
    {
        public Guid? ProductVariantId { get; set; }

        public Guid? PropertyId { get; set; }

        public double Value { get; set; }
    }
}
