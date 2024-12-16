using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using PimApi.Repositories.Products;
using Shared.Controllers;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-boolean-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantBooleanPropertyController : DefaultControllerTemplate<ProductVariantBooleanProperty, ViewProductVariantBooleanProperty, CreateProductVariantBooleanProperty, PatchProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
    {
        public ProductVariantBooleanPropertyController(IMapper mapper, IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters> repository) : base(mapper, repository)
        {
        }
    }
}
