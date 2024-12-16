using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public class ProductVariantStringPropertySearchParameters : SearchParameters
    {
        [SwaggerParameter("Comma separated list of guids.")]
        public string? ProductVariantIds { get; set; }

        [SwaggerParameter("Comma separated list of guids.")]
        public string? PropertyIds { get; set; }

        public string? Value { get; set; }
    }
}
