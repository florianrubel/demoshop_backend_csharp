using LibDb.Models.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibUniversal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class DbControllerTemplate : BasicControllerTemplate
    {
        protected virtual void SetPaginationHeaders(IPagedList pagedList)
        {
            Response.Headers.Append("Pagination.TotalCount", pagedList.TotalCount.ToString());
            Response.Headers.Append("Pagination.PageSize", pagedList.PageSize.ToString());
            Response.Headers.Append("Pagination.Page", pagedList.Page.ToString());
            Response.Headers.Append("Pagination.TotalPages", pagedList.TotalPages.ToString());
        }

    }
}
