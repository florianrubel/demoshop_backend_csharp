using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ProductVariantPaginationParameters : PaginationParameters
    {
        [SwaggerParameter("Comma separated list of guids.")]
        public string? ProductIds { get; set; }
    }
}
