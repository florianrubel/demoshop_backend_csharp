using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PimApi.Cache;
using Shared.Models.Api;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.Product;
using SharedProducts.Repositories.Write.Products;

namespace PimApi.Controllers.Products
{
    [Route("products")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductController : AbstractProductCacheController<Product, ViewProduct, CreateProduct, PatchProduct, SearchParameters>
    {
        public ProductController(
            IMapper mapper,
            IProductRepository<Product, SearchParameters> repository,
            IProductCacheFactory productCacheFactory
        ) : base(mapper, repository, productCacheFactory)
        { }

        public async override Task<ActionResult<IEnumerable<ViewProduct>>> Create([FromBody] IEnumerable<CreateProduct> createObjs)
        {
            var result = await base.Create(createObjs);

            _productCacheFactory.BuildByProducts(from record in result.Value select record.Id);

            return result;
        }

        public async override Task<ActionResult<Dictionary<Guid, ViewProduct>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchProduct>> patchDocuments)
        {
            var result = await base.Patch(patchDocuments);

            _productCacheFactory.BuildByProducts(result.Value.Keys);

            return result;
        }
    }
}
