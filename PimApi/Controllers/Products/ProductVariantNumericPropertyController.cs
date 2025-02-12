using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-numeric-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantNumericPropertyController : WithDeleteDefaultControllerTemplate<ProductVariantNumericProperty, ViewProductVariantNumericProperty, CreateProductVariantNumericProperty, PatchProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
    {
        public ProductVariantNumericPropertyController(
            IMapper mapper,
            IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters> repository
        ) : base(mapper, repository)
        { }
    }
}
