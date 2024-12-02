using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantBooleanProperty
{
    public interface IProductVariantBooleanPropertyPaginationParameters : IPaginationParameters
    {
        string? ProductVariantIds { get; set; }
        string? PropertyIds { get; set; }
        bool? Value { get; set; }
    }
}