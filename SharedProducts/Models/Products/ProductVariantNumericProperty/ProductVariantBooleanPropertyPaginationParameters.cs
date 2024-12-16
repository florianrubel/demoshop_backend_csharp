using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SharedProducts.Models.Products.ProductVariantNumericProperty
{
    public class ProductVariantNumericPropertyPaginationParameters : PaginationParameters
    {
        [SwaggerParameter("Comma separated list of guids.")]
        public string? ProductVariantIds { get; set; }

        [SwaggerParameter("Comma separated list of guids.")]
        public string? PropertyIds { get; set; }

        public double? Value { get; set; }
    }
}
