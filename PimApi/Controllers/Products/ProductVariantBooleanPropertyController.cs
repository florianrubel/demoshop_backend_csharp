using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-boolean-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantBooleanPropertyController : WithDeleteDefaultControllerTemplate<ProductVariantBooleanProperty, ViewProductVariantBooleanProperty, CreateProductVariantBooleanProperty, PatchProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
    {
        public ProductVariantBooleanPropertyController(
            IMapper mapper,
            IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters> repository
        ) : base(mapper, repository)
        { }
    }


}
