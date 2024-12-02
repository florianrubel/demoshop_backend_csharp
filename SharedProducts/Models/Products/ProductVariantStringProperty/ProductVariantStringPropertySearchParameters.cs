using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public class ProductVariantStringPropertySearchParameters : SearchParameters, IProductVariantStringPropertySearchParameters
    {
        public string? ProductVariantIds { get; set; }

        public string? PropertyIds { get; set; }

        public string? Value { get; set; }
    }
}
