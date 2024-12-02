using Shared.Models.Api;

namespace SharedProducts.Models.Products.ProductVariantBooleanProperty
{
    public class ProductVariantBooleanPropertyPaginationParameters : PaginationParameters, IProductVariantBooleanPropertyPaginationParameters
    {
        public string? ProductVariantIds { get; set; }

        public string? PropertyIds { get; set; }

        public bool? Value { get; set; }
    }
}
