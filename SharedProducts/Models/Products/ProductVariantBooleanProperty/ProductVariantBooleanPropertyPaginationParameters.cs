using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SharedProducts.Models.Products.ProductVariantBooleanProperty
{
    public class ProductVariantBooleanPropertyPaginationParameters : PaginationParameters
    {
        [SwaggerParameter("Comma separated list of guids.")]
        public string? ProductVariantIds { get; set; }

        [SwaggerParameter("Comma separated list of guids.")]
        public string? PropertyIds { get; set; }

        public bool? Value { get; set; }
    }
}
