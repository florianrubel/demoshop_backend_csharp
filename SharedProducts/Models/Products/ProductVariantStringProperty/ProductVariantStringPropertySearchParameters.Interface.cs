using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public interface IProductVariantStringPropertySearchParameters : ISearchParameters
    {
        string? ProductVariantIds { get; set; }
        string? PropertyIds { get; set; }
        string? Value { get; set; }
    }
}