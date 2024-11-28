using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ProductVariantSearchParameters : SearchParameters
    {
        public string? ProductIds { get; set; }
    }
}
