using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantNumericProperty
{
    public interface IProductVariantNumericPropertyPaginationParameters : IPaginationParameters
    {
        string? ProductVariantIds { get; set; }
        string? PropertyIds { get; set; }
        double? Value { get; set; }
    }
}