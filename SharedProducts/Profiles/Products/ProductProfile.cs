using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.Product;
using Shared.Profiles;

namespace SharedProducts.Profiles.Products
{
    public class ProductProfile : DefaultProfile<Product, ViewProduct, CreateProduct, PatchProduct>
    {
    }
}
