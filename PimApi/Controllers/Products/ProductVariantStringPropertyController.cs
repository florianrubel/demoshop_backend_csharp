using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using SharedProducts.Repositories.Write.Products;
using System.Text.Json;

namespace PimApi.Controllers.Products
{
    [Route("product-variant-string-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductVariantStringPropertyController : WithDeleteDefaultControllerTemplate<ProductVariantStringProperty, ViewProductVariantStringProperty, CreateProductVariantStringProperty, PatchProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
    {
        public ProductVariantStringPropertyController(
            IMapper mapper,
            IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters> repository
        ) : base(mapper, repository)
        { }

        [HttpPatch]
        [Route("")]
        public override async Task<ActionResult<Dictionary<Guid, ViewProductVariantStringProperty>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchProductVariantStringProperty>> patchDocuments)
        {
            var results = new Dictionary<Guid, ViewProductVariantStringProperty>();

            foreach (KeyValuePair<Guid, JsonPatchDocument<PatchProductVariantStringProperty>> pair in patchDocuments)
            {
                var id = pair.Key;
                var patchDocument = pair.Value;
                var entity = await _repository.GetOneOrDefault(id);
                var oldEntity = JsonSerializer.Deserialize<ProductVariantStringProperty>(JsonSerializer.Serialize(entity));

                if (entity == null) return NotFound();

                PatchProductVariantStringProperty patchObj = _mapper.Map<PatchProductVariantStringProperty>(entity);
                patchDocument.ApplyTo(patchObj, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(patchObj, entity);
                await _repository.Update(entity, oldEntity);
                results.Add(id, _mapper.Map<ViewProductVariantStringProperty>(entity));
            }

            return Ok(results);
        }
    }
}
