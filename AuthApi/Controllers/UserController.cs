﻿using AuthApi.Models.Identity.User;
using AuthApi.Repositories.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Helpers;
using Shared.Models.Api;

namespace AuthApi.Controllers
{
    [Route("[Controller]")]
    public class UserController : BasicControllerTemplate
    {
        private const string AUTHORIZE_MIN_ADMIN = $"{Shared.Constants.Identity.ROLE_SUPERADMIN},{Shared.Constants.Identity.ROLE_SUPERADMIN}";
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

        /// <summary>
        /// Get a paginated list with filters.
        /// </summary>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Superadmin")]
        public virtual async Task<ActionResult<PagedList<ViewUser>>> GetMultiple([FromQuery] SearchParameters parameters)
        {
            var entities = await _userRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(_mapper.Map<IEnumerable<ViewUser>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get a list with multiple ids.
        /// </summary>
        [HttpGet]
        [Route("({ids})")]
        [Authorize(Roles = AUTHORIZE_MIN_ADMIN)]
        public virtual async Task<ActionResult<IEnumerable<ViewUser>>> GetMultiple(
            [FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids,
            [FromQuery] ShapingWithOrderingParameters parameters
        )
        {
            if (ids == null)
                return BadRequest();

            var entities = await _userRepository.GetMultiple(from id in ids select id.ToString(), parameters);

            return Ok(_mapper.Map<IEnumerable<ViewUser>>(entities).ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Get by id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = AUTHORIZE_MIN_ADMIN)]
        public virtual async Task<ActionResult<ViewUser>> GetOne(Guid id, [FromQuery] string? fields)
        {
            var entity = await _userRepository.GetOneOrDefault(id.ToString());

            if (entity == null) return NotFound();

            return Ok(_mapper.Map<ViewUser>(entity).ShapeData(fields));
        }
    }
}
