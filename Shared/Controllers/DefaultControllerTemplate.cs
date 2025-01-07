using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Api;
using AutoMapper;
using Shared.Repositories;
using Shared.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Shared.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class DefaultControllerTemplate<EntityType, ViewType, CreateType, PatchType, PaginationParametersType> : BasicControllerTemplate
        where EntityType : UuidBaseEntity
        where PatchType : class
        where PaginationParametersType : PaginationParameters
    {
        protected readonly IUuidBaseRepository<EntityType, PaginationParametersType> _repository;
        protected readonly IMapper _mapper;

        public DefaultControllerTemplate(
            IMapper mapper,
            IUuidBaseRepository<EntityType, PaginationParametersType> repository
        )
        {
            _mapper = mapper;
            _repository = repository;
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
        public virtual async Task<ActionResult<PagedList<ViewType>>> GetMultiple([FromQuery] PaginationParametersType parameters)
        {
            var entities = await _repository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(entities).ShapeData(parameters.Fields));
        }

        [HttpGet]
        [Route("({ids})")]
        [SwaggerOperation(
            "Get multiple by ids",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Get a list by providing a comma separated list of guids within the bracelets."
        )]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))][SwaggerParameter(
                "<p>Comma separated list of guids</p><code>(4aec62a1-c12d-405b-8902-3c2388f2bd76,01b09797-b9fe-43d4-878c-4010cb9c90c8)</code>"
            )] IEnumerable<Guid> ids,
            [FromQuery] ShapingWithOrderingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            var entities = await _repository.GetMultipleByIds(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(entities).ShapeData(parameters.Fields));
        }

        [HttpPost]
        [Route("ids")]
        [SwaggerOperation(
            "Get multiple by ids",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Get a list by providing a comma separated list of guids in the request body." +
            "Use this method if you requesting too many ids and the url would get too long." +
            "<p>Example</p>" +
            "<pre><code>" +
            "[\n" +
            "   \"4aec62a1-c12d-405b-8902-3c2388f2bd76\",\n" +
            "   \"01b09797-b9fe-43d4-878c-4010cb9c90c8\",\n" +
            "]" +
            "</code></pre>"
        )]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> GetMultipleByPost(
            [FromBody] IEnumerable<Guid> ids,
            [FromQuery] ShapingWithOrderingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            var entities = await _repository.GetMultipleByIds(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(entities).ShapeData(parameters.Fields));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(
            "Get one by id",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Get one for the given id."
        )]
        public virtual async Task<ActionResult<ViewType>> GetOne(Guid id, [FromQuery] ShapingParameters parameters)
        {
            var entity = await _repository.GetOneOrDefault(id);

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ViewType>(entity).ShapeData(parameters.Fields));
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(
            "Create multiple",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Create multiple objects by sending them in an array.</p>" +
            "<p>Example</p>" +
            "<pre><code>" +
            "[\n" +
            "    {\n" +
            "        \"prop1\": \"value1\",\n" +
            "        \"prop2\": 2,\n" +
            "        \"prop3\": true\n" +
            "    }\n" +
            "]" +
            "</code></pre>"
        )]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> Create([FromBody] IEnumerable<CreateType> createObjs)
        {
            var newEntities = _mapper.Map<IEnumerable<EntityType>>(createObjs);
            newEntities = await _repository.CreateRange(newEntities);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(newEntities));
        }

        [HttpPatch]
        [Route("")]
        [SwaggerOperation(
            "Patch multiple",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Patch multiple objects by sending an object with the guid as the key and an array of patch operations.</p>" +
            "<p>This prevents overwriting whole objects and allows updating specific properties only.</p>" +
            "<p>Example</p>" +
            "<pre><code>" +
            "{\n" +
            "    \"e0f81c5b-9d06-475e-8691-92aa6ccb166f\": [\n" +
            "        { \"op\": \"replace\", \"path\": \"prop1\", \"value\": \"value1\" },\n" +
            "        { \"op\": \"replace\", \"path\": \"prop2\", \"value\": 2 },\n" +
            "        { \"op\": \"replace\", \"path\": \"prop3\", \"value\": true }\n" +
            "    ]\n" +
            "}\n" +
            "</code></pre>"
        )]
        public virtual async Task<ActionResult<Dictionary<Guid, ViewType>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchType>> patchDocuments)
        {
            var results = new Dictionary<Guid, ViewType>();

            foreach (KeyValuePair<Guid, JsonPatchDocument<PatchType>> pair in patchDocuments)
            {
                var id = pair.Key;
                var patchDocument = pair.Value;
                var entity = await _repository.GetOneOrDefault(id);

                if (entity == null) return NotFound();

                PatchType patchObj = _mapper.Map<PatchType>(entity);
                patchDocument.ApplyTo(patchObj, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(patchObj, entity);
                await _repository.Update(entity);
                results.Add(id, _mapper.Map<ViewType>(entity));
            }

            return Ok(results);
        }
    }
}
