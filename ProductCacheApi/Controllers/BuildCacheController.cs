using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCacheApi.Cache;

namespace ProductCacheApi.Controllers
{

    [Route("build-cache")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class HomeController : Shared.Controllers.BasicControllerTemplate
    {
        private readonly IProductCacheFactory _productCacheFactory;

        public HomeController(IProductCacheFactory productCacheFactory)
        {
            _productCacheFactory = productCacheFactory;
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Index()
        {
            await _productCacheFactory.Build();
            return Ok();
        }

        [Route("by-products")]
        [HttpPost]
        public async Task<ActionResult> ByProducts([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProducts(ids);
            return Ok();
        }

        [Route("by-product-variants")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariants([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariants(ids);
            return Ok();
        }

        [Route("by-boolean-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByBooleanProperty(Guid id)
        {
            await _productCacheFactory.BuildByBooleanProperty(id);
            return Ok();
        }

        [Route("by-boolean-properties")]
        [HttpPost]
        public async Task<ActionResult> ByBooleanProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByBooleanProperties(ids);
            return Ok();
        }

        [Route("by-numeric-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByNumericProperty(Guid id)
        {
            await _productCacheFactory.BuildByNumericProperty(id);
            return Ok();
        }

        [Route("by-numeric-properties")]
        [HttpPost]
        public async Task<ActionResult> ByNumericProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByNumericProperties(ids);
            return Ok();
        }

        [Route("by-string-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByStringProperty(Guid id)
        {
            await _productCacheFactory.BuildByStringProperty(id);
            return Ok();
        }

        [Route("by-string-properties")]
        [HttpPost]
        public async Task<ActionResult> ByStringProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByStringProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-boolean-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantBooleanProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantBooleanProperty(id);
            return Ok();
        }

        [Route("by-product-variant-boolean-properties")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantBooleanProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantBooleanProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-numeric-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantNumericProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantNumericProperty(id);
            return Ok();
        }

        [Route("by-product-variant-numeric-properties")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantNumericProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantNumericProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-string-property/{id}")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantStringProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantStringProperty(id);
            return Ok();
        }

        [Route("by-product-variant-string-properties")]
        [HttpPost]
        public async Task<ActionResult> ByProductVariantStringProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantStringProperties(ids);
            return Ok();
        }
    }
}
