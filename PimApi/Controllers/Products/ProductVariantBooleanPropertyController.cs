using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PimApi.Cache;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-boolean-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantBooleanPropertyController : AbstractWithDeleteProductCacheController<ProductVariantBooleanProperty, ViewProductVariantBooleanProperty, CreateProductVariantBooleanProperty, PatchProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
    {
        public ProductVariantBooleanPropertyController(
            IMapper mapper,
            IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters> repository,
            IProductCacheFactory productCacheFactory
        ) : base(mapper, repository, productCacheFactory)
        { }

        public async override Task<ActionResult<IEnumerable<ViewProductVariantBooleanProperty>>> Create([FromBody] IEnumerable<CreateProductVariantBooleanProperty> createObjs)
        {
            var result = await base.Create(createObjs);

            _productCacheFactory.BuildByBooleanProperties(from record in result.Value select record.Id);

            return result;
        }

        public async override Task<ActionResult<Dictionary<Guid, ViewProductVariantBooleanProperty>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchProductVariantBooleanProperty>> patchDocuments)
        {
            var result = await base.Patch(patchDocuments);

            _productCacheFactory.BuildByBooleanProperties(result.Value.Keys);

            return result;
        }
    }


}
