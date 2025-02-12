using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variants")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantController : DefaultControllerTemplate<ProductVariant, ViewProductVariant, CreateProductVariant, PatchProductVariant, ProductVariantPaginationParameters>
    {
        public ProductVariantController(
            IMapper mapper,
            IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters> repository
        ) : base(mapper, repository)
        { }
    }
}
