using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCacheApi.Cache;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductCacheApi.Controllers
{

    [Route("build-cache")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class BuildCacheController : Shared.Controllers.BasicControllerTemplate
    {
        private readonly IProductCacheFactory _productCacheFactory;

        public BuildCacheController(IProductCacheFactory productCacheFactory)
        {
            _productCacheFactory = productCacheFactory;
        }

        [Route("")]
        [HttpPost]
        [SwaggerOperation(
            "Build cache",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the entire product database."
        )]
        public async Task<ActionResult> Index()
        {
            await _productCacheFactory.Build();
            return Ok();
        }

        [Route("by-products")]
        [HttpPost]
        [SwaggerOperation(
            "> by products",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByProducts([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProducts(ids);
            return Ok();
        }

        [Route("by-product-variants")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variants",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant ids.</p>" +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByProductVariants([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariants(ids);
            return Ok();
        }

        [Route("by-boolean-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by boolean property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided boolean property id."
        )]
        public async Task<ActionResult> ByBooleanProperty(Guid id)
        {
            await _productCacheFactory.BuildByBooleanProperty(id);
            return Ok();
        }

        [Route("by-boolean-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by boolean properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided boolean property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByBooleanProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByBooleanProperties(ids);
            return Ok();
        }

        [Route("by-numeric-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by numeric property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided numeric property id."
        )]
        public async Task<ActionResult> ByNumericProperty(Guid id)
        {
            await _productCacheFactory.BuildByNumericProperty(id);
            return Ok();
        }

        [Route("by-numeric-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by numeric properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided numeric property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByNumericProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByNumericProperties(ids);
            return Ok();
        }

        [Route("by-string-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by string property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided string property id."
        )]
        public async Task<ActionResult> ByStringProperty(Guid id)
        {
            await _productCacheFactory.BuildByStringProperty(id);
            return Ok();
        }

        [Route("by-string-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by string properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided string property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByStringProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByStringProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-boolean-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant boolean property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant boolean property id."
        )]
        public async Task<ActionResult> ByProductVariantBooleanProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantBooleanProperty(id);
            return Ok();
        }

        [Route("by-product-variant-boolean-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant boolean properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant boolean property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByProductVariantBooleanProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantBooleanProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-numeric-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant numeric property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant numeric property id."
        )]
        public async Task<ActionResult> ByProductVariantNumericProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantNumericProperty(id);
            return Ok();
        }

        [Route("by-product-variant-numeric-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant numeric properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant numeric property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByProductVariantNumericProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantNumericProperties(ids);
            return Ok();
        }

        [Route("by-product-variant-string-property/{id}")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant string property",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant string property id."
        )]
        public async Task<ActionResult> ByProductVariantStringProperty(Guid id)
        {
            await _productCacheFactory.BuildByProductVariantStringProperty(id);
            return Ok();
        }

        [Route("by-product-variant-string-properties")]
        [HttpPost]
        [SwaggerOperation(
            "> by product variant string properties",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Build the cache for the provided product variant string property ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"
        )]
        public async Task<ActionResult> ByProductVariantStringProperties([FromBody] IEnumerable<Guid> ids)
        {
            await _productCacheFactory.BuildByProductVariantStringProperties(ids);
            return Ok();
        }
    }
}
