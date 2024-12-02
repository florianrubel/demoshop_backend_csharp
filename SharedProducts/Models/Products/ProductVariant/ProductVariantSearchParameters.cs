using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ProductVariantSearchParameters : SearchParameters, IProductVariantSearchParameters
    {
        public string? ProductIds { get; set; }
    }
}
