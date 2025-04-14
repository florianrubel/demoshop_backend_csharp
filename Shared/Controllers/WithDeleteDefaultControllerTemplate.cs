using AutoMapper;
using LibDb.Repositories;
using LibUniversal.Entities;
using LibUniversal.Models.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LibDb.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class WithDeleteDefaultControllerTemplate<EntityType, ViewType, CreateType, PatchType, PaginationParametersType>
        : DefaultControllerTemplate<EntityType, ViewType, CreateType, PatchType, PaginationParametersType>
        where EntityType : UuidBaseEntity
        where PatchType : class
        where PaginationParametersType : PaginationParameters
    {
        public WithDeleteDefaultControllerTemplate(
            IMapper mapper,
            IUuidBaseRepository<EntityType, PaginationParametersType> repository
        ) : base(mapper, repository)
        { }

        [HttpDelete]
        [Route("")]
        [SwaggerOperation(
            "Delete multiple by ids",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Delete by providing a comma separated list of guids in the request body." +
            "<p>Example</p>" +
            "<pre><code>" +
            "[\n" +
            "   \"4aec62a1-c12d-405b-8902-3c2388f2bd76\",\n" +
            "   \"01b09797-b9fe-43d4-878c-4010cb9c90c8\",\n" +
            "]" +
            "</code></pre>"
        )]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> Delete(
            [FromBody] IEnumerable<Guid> ids
        )
        {
            if (ids == null)
                return BadRequest();

            await _repository.DeleteRange(ids);

            return Ok();
        }
    }
}
