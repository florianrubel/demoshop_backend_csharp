using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariant
{
    public interface IProductVariantPaginationParameters : IPaginationParameters
    {
        string? ProductIds { get; set; }
    }
}