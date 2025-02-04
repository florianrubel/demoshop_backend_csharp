using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Api;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.BooleanProperty;
using SharedProducts.Repositories.Write.Products.Properties;

namespace PimApi.Controllers.Products.Properties
{
    [Route("properties/boolean-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class BooleanPropertyController : DefaultControllerTemplate<BooleanProperty, ViewBooleanProperty, CreateBooleanProperty, PatchBooleanProperty, SearchParameters>
    {
        public BooleanPropertyController(IMapper mapper, IBooleanPropertyRepository<BooleanProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }

        public override async Task<ActionResult<IEnumerable<ViewBooleanProperty>>> Create([FromBody] IEnumerable<CreateBooleanProperty> createObjs)
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

        public override async Task<ActionResult<Dictionary<Guid, ViewBooleanProperty>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchBooleanProperty>> patchDocuments)
        {
            var existing = await _repository.GetMultiple(new SearchParameters { PageSize = -1 });
            var conflicts = new Dictionary<string, List<string>>();
            conflicts.Add("name", new List<string>());

            foreach (var item in patchDocuments)
            {
                var patchObj = _mapper.Map<PatchBooleanProperty>(item.Value);
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
