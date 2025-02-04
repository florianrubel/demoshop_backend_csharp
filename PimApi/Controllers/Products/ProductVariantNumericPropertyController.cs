using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PimApi.Cache;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-numeric-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantNumericPropertyController : AbstractWithDeleteProductCacheController<ProductVariantNumericProperty, ViewProductVariantNumericProperty, CreateProductVariantNumericProperty, PatchProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
    {
        public ProductVariantNumericPropertyController(
            IMapper mapper,
            IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters> repository,
            IProductCacheFactory productCacheFactory
        ) : base(mapper, repository, productCacheFactory)
        { }

        public async override Task<ActionResult<IEnumerable<ViewProductVariantNumericProperty>>> Create([FromBody] IEnumerable<CreateProductVariantNumericProperty> createObjs)
        {
            var result = await base.Create(createObjs);

            _productCacheFactory.BuildByProductVariantNumericProperties(from record in result.Value select record.Id);

            return result;
        }

        public async override Task<ActionResult<Dictionary<Guid, ViewProductVariantNumericProperty>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchProductVariantNumericProperty>> patchDocuments)
        {
            var result = await base.Patch(patchDocuments);

            _productCacheFactory.BuildByProductVariantNumericProperties(result.Value.Keys);

            return result;
        }
    }
}
