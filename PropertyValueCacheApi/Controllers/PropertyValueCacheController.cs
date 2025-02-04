using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PropertyValueCacheApi.Hubs;
using Shared.Controllers;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using SharedProducts.Repositories.ReadOnly.Products;
using SharedPropertyValueCache.Services;

namespace PropertyValueCacheApi.Controllers
{
    [ApiController]
    [Route("property-value-cache")]
    public class PropertyValueCacheController : BasicControllerTemplate
    {
        private readonly IPropertyValueCacheService _propertyValueCacheService;
        private readonly IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters> _productVariantStringPropertyRepository;
        private readonly IHubContext<PropertyValueCacheHub> _hubContext;

        public PropertyValueCacheController(
            IPropertyValueCacheService propertyValueCacheService,
            IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters> productVariantStringPropertyRepository,
            IHubContext<PropertyValueCacheHub> hubContext
        )
        {
            _propertyValueCacheService = propertyValueCacheService;
            _productVariantStringPropertyRepository = productVariantStringPropertyRepository;
            _hubContext = hubContext;
        }

        [Route("build-cache")]
        [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
        [HttpPost]
        public async Task<ActionResult> BuildCache()
        {
            var relations = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters
            {
                PageSize = -1,
            });

            var propertyIds = (from relation in relations select relation.PropertyId).Distinct();

            foreach (var item in propertyIds.Select((propertyId, i) => new { i, propertyId }))
            {
                var values = (from relation in relations where relation.PropertyId == item.propertyId select relation.Value).Distinct();
                await _propertyValueCacheService.SetValuesForProperty(item.propertyId, values);
                await _hubContext.Clients.All.SendAsync("cache-progress", new
                {
                    current = item.i,
                    count = propertyIds.Count()
                });
            }

            return Ok();
        }
    }
}
