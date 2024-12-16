using AuthApi.Models.Identity.User;
using AuthApi.Repositories.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Helpers;
using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthApi.Controllers
{
    public class TestQuery
    {
        public int Page { get; set; }
    }

    [Route("user")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class UserController : BasicControllerTemplate
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserController(
            IMapper mapper,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(
            "Paginated List",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Get a paginated list with the given page size(default 25) and filter by any given filter parameter.</p>" +
            "<p>The pagination data is provided in the response header</p>" +
            "<table>" +
            "<tr><td>Pagination.Page</td><td>The current page</td></tr>" +
            "<tr><td>Pagination.PageSize</td><td>The requested page size or default</td></tr>" +
            "<tr><td>Pagination.TotalPages</td><td>The max number of pages of the result</td></tr>" +
            "<tr><td>Pagination.TotalCount</td><td>The total count of records of the result</td></tr>" +
            "</table>"
        )]
        public virtual async Task<ActionResult<PagedList<ViewUser>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            var entities = await _userRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewUser>>(entities).ShapeData(parameters.Fields));
        }

        [HttpGet]
        [Route("({ids})")]
        [SwaggerOperation(
            "Get multiple by ids",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Get a list by providing a comma separated list of guids within the bracelets."
        )]
        public virtual async Task<ActionResult<IEnumerable<ViewUser>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))][SwaggerParameter(
                "<p>Comma separated list of guids</p><code>(4aec62a1-c12d-405b-8902-3c2388f2bd76,01b09797-b9fe-43d4-878c-4010cb9c90c8)</code>"
            )] IEnumerable<Guid> ids,
            [FromQuery] ShapingWithOrderingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            var entities = await _userRepository.GetMultiple(from id in ids select id.ToString(), parameters);

            return Ok(_mapper.Map<IEnumerable<ViewUser>>(entities).ShapeData(parameters.Fields));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(
            "Get one by id",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Get one for the given id."
        )]
        public virtual async Task<ActionResult<ViewUser>> GetOne(Guid id, [FromQuery] string? fields)
        {
            var entity = await _userRepository.GetOneOrDefault(id.ToString());

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ViewUser>(entity).ShapeData(fields));
        }
    }
}
