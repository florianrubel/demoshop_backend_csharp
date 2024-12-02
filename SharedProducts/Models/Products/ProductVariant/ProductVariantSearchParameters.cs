using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class ProductVariantSearchParameters : PaginationParameters, IProductVariantPaginationParameters
    {
        public string? ProductIds { get; set; }
    }
}
