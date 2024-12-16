using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models.Api
{
    public class SearchParameters : PaginationParameters
    {
        [SwaggerParameter("Case insensitive text search for specific properties")]
        public string SearchQuery { get; set; }
    }
}
