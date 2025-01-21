using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SharedProducts.Models.Products
{
    public abstract class AbstractProductVariantSearchParameters<ValueType> : SearchParameters
    {
        [SwaggerParameter("Comma separated list of guids.")]
        public string? ProductVariantIds { get; set; }

        [SwaggerParameter("Comma separated list of guids.")]
        public string? PropertyIds { get; set; }

        public ValueType Value { get; set; }
    }
}
