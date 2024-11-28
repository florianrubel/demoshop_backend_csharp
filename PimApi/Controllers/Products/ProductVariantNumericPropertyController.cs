using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using PimApi.Repositories.Products;
using Shared.Controllers;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-numeric-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantNumericPropertyController : DefaultControllerTemplate<ProductVariantNumericProperty, ViewProductVariantNumericProperty, CreateProductVariantNumericProperty, PatchProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
    {
        public ProductVariantNumericPropertyController(IMapper mapper, IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters> repository) : base(mapper, repository)
        {
        }
    }
}
