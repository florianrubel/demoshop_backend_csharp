using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PimApi.Cache;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("product-variants")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantController : AbstractProductCacheController<ProductVariant, ViewProductVariant, CreateProductVariant, PatchProductVariant, ProductVariantPaginationParameters>
    {
        public ProductVariantController(
            IMapper mapper,
            IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters> repository,
            IProductCacheFactory productCacheFactory
        ) : base(mapper, repository, productCacheFactory)
        { }

        public async override Task<ActionResult<IEnumerable<ViewProductVariant>>> Create([FromBody] IEnumerable<CreateProductVariant> createObjs)
        {
            var result = await base.Create(createObjs);

            _productCacheFactory.BuildByProductVariants(from record in result.Value select record.Id);

            return result;
        }

        public async override Task<ActionResult<Dictionary<Guid, ViewProductVariant>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchProductVariant>> patchDocuments)
        {
            var result = await base.Patch(patchDocuments);

            _productCacheFactory.BuildByProducts(result.Value.Keys);

            return result;
        }
    }
}
