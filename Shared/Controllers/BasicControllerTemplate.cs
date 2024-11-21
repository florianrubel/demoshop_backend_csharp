using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Api;
using System.IdentityModel.Tokens.Jwt;

namespace Shared.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class BasicControllerTemplate : ControllerBase
    {
        protected virtual void SetPaginationHeaders(IPagedList pagedList)
        {
            Response.Headers.Append("Pagination.TotalCount", pagedList.TotalCount.ToString());
            Response.Headers.Append("Pagination.PageSize", pagedList.PageSize.ToString());
            Response.Headers.Append("Pagination.Page", pagedList.Page.ToString());
            Response.Headers.Append("Pagination.TotalPages", pagedList.TotalPages.ToString());
        }

        protected virtual bool CurrentUserHasRole(string role)
        {
            return HttpContext.User.IsInRole(role);
        }

        protected virtual string? GetCurrentUserId()
        {
            return (from claim in HttpContext.User.Claims where claim.Type == JwtRegisteredClaimNames.Sub select claim.Value).FirstOrDefault();
        }
    }
}
