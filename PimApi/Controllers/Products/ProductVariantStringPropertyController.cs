using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using PimApi.Repositories.Products;
using Shared.Controllers;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-string-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantStringPropertyController : DefaultControllerTemplate<ProductVariantStringProperty, ViewProductVariantStringProperty, CreateProductVariantStringProperty, PatchProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
    {
        public ProductVariantStringPropertyController(IMapper mapper, IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
