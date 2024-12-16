using AuthApi.Entities.Authentication;
using AuthApi.Models.Authentication;
using AuthApi.Models.Authentication.ApiKey;
using AuthApi.Repositories.Authentication;
using AuthApi.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Helpers;
using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace AuthApi.Controllers
{
    [Route("api-key")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ApiKeyController : BasicControllerTemplate
    {
        private readonly IApiKeyRepository<ApiKey, PaginationParameters> _apiKeyRepository;

        public IApiTokenService _apiTokenService { get; }

        public ApiKeyController(
            IApiKeyRepository<ApiKey, PaginationParameters> apiKeyRepository,
            IApiTokenService apiTokenService
        )
        {
            _apiKeyRepository = apiKeyRepository;
            _apiTokenService = apiTokenService;
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
        public async Task<ActionResult<PagedList<ApiKey>>> GetMultiple([FromQuery] PaginationParameters parameters)
        {
            var entities = await _apiKeyRepository.GetMultiple(parameters);
            SetPaginationHeaders(entities);

            return Ok(entities.ShapeData(parameters.Fields));
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation(
            "Create one",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Create a single api key for a specific scope."
        )]
        public async Task<ActionResult<ApiKey>> Create([FromBody] ICreateApiKey createApiToken)
        {
            var token = _apiTokenService.GenerateToken(createApiToken.Scope, createApiToken.Roles ?? new List<string>());

            var apiKey = await _apiKeyRepository.Create(new ApiKey
            {
                Key = token
            });

            return Ok(apiKey);
        }

        [SwaggerOperation(
            "Is valid",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "Check if an api key is still valid."
        )]
        [HttpPost]
        [Route("is-valid")]
        public ActionResult IsValid([FromBody] ICheckApiKeyRequest checkApiKeyRequest)
        {
            var token = _apiKeyRepository.GetOneOrDefaultByToken(checkApiKeyRequest.ApiKey);

            if (token == null) return Unauthorized();

            return Ok();
        }
    }
}
