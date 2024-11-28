using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantNumericProperty
{
    public class ProductVariantNumericPropertyPaginationParameters : PaginationParameters
    {
        public string? ProductVariantIds { get; set; }

        public string? PropertyIds { get; set; }

        public double? Value { get; set; }
    }
}
