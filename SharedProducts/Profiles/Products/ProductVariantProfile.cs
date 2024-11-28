using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using Shared.Profiles;

namespace SharedProducts.Profiles.Products
{
    public class ProductVariantProfile : DefaultProfile<ProductVariant, ViewProductVariant, CreateProductVariant, PatchProductVariant>
    {
    }
}
