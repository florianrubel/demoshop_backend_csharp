using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace LibUniversal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public abstract class BasicControllerTemplate : ControllerBase
    {
        protected virtual bool CurrentUserHasRole(string role)
        {
            return HttpContext.User.IsInRole(role);
        }

        protected virtual string? GetCurrentUserId()
        {
            return (from claim in HttpContext.User.Claims where claim.Type == JwtRegisteredClaimNames.Sub select claim.Value).FirstOrDefault();
        }

        protected virtual string? GetUserToken()
        {
            string authorizationHeader = Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }
    }
}
