using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Api;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.StringProperty;
using SharedProducts.Repositories.Write.Products.Properties;

namespace PimApi.Controllers.Products.Properties
{
    [Route("properties/string-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class StringPropertyController : DefaultControllerTemplate<StringProperty, ViewStringProperty, CreateStringProperty, PatchStringProperty, SearchParameters>
    {
        public StringPropertyController(IMapper mapper, IStringPropertyRepository<StringProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }

        [ProducesResponseType(typeof(IEnumerable<ViewStringProperty>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public override async Task<ActionResult<IEnumerable<ViewStringProperty>>> Create([FromBody] IEnumerable<CreateStringProperty> createObjs)
        {
            var existing = await _repository.GetMultiple(new SearchParameters { PageSize = -1 });
            var conflicts = new Dictionary<string, List<string>>();
            conflicts.Add("name", new List<string>());
            foreach (var item in createObjs)
            {
                if (existing.Where(r => r.Name == item.Name).Count() > 0)
                {
                    conflicts["name"].Add(item.Name);
                }
            }
            if (conflicts["name"].Count() > 0)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors.Add(new ApiError
                {
                    ErrorCode = Shared.Constants.Errors.ERROR_CONFLICT,
                    Conflicts = conflicts,
                });
                return Conflict(errorResponse);
            }
            return await base.Create(createObjs);
        }

        [ProducesResponseType(typeof(Dictionary<Guid, ViewStringProperty>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public override async Task<ActionResult<Dictionary<Guid, ViewStringProperty>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchStringProperty>> patchDocuments)
        {
            var existing = await _repository.GetMultiple(new SearchParameters { PageSize = -1 });
            var conflicts = new Dictionary<string, List<string>>();
            conflicts.Add("name", new List<string>());

            foreach (var item in patchDocuments)
            {
                var patchObj = _mapper.Map<PatchStringProperty>(item.Value);
                item.Value.ApplyTo(patchObj, ModelState);
                if (patchObj.Name == null) continue;

                if (existing.Where(r => r.Name == patchObj.Name && item.Key != r.Id).Count() > 0)
                {
                    conflicts["name"].Add(patchObj.Name);
                }
            }
            if (conflicts["name"].Count() > 0)
            {
                var errorResponse = new ErrorResponse();
                errorResponse.Errors.Add(new ApiError
                {
                    ErrorCode = Shared.Constants.Errors.ERROR_CONFLICT,
                    Conflicts = conflicts,
                });
                return Conflict(errorResponse);
            }

            return await base.Patch(patchDocuments);
        }
    }
}
