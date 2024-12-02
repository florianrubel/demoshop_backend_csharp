using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariant
{
    public interface IProductVariantSearchParameters : ISearchParameters
    {
        string? ProductIds { get; set; }
    }
}