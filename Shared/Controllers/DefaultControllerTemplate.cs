using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Api;
using AutoMapper;
using Shared.Repositories;
using Shared.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Shared.Helpers;

namespace Shared.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class DefaultControllerTemplate<EntityType, ViewType, CreateType, PatchType, PaginationParametersType> : BasicControllerTemplate
        where EntityType : UuidBaseEntity
        where PatchType : class
        where PaginationParametersType : IPaginationParameters
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

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        public virtual async Task<ActionResult<PagedList<ViewType>>> GetMultiple([FromQuery] PaginationParametersType parameters)
        {
            var entities = await _repository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingWithOrderingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            var entities = await _repository.GetMultipleByIds(ids, parameters);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult<ViewType>> GetOne(Guid id, [FromQuery] string? fields)
        {
            var entity = await _repository.GetOneOrDefault(id);

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ViewType>(entity).ShapeData(fields));
        }

        /// <summary>
        /// Create multiple new ones.
        /// </summary>
        [HttpPost]
        [Route("")]
        public virtual async Task<ActionResult<IEnumerable<ViewType>>> Create([FromBody] IEnumerable<CreateType> createObjs)
        {
            var newEntities = _mapper.Map<IEnumerable<EntityType>>(createObjs);
            newEntities = await _repository.CreateRange(newEntities);

            return Ok(_mapper.Map<IEnumerable<ViewType>>(newEntities));
        }

        /// <summary>
        /// Patch several existing ones.
        /// </summary>
        [HttpPatch]
        [Route("")]
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
