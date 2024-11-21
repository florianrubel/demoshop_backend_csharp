namespace Shared.Models.Api
{
    public class SearchParameters : PaginationParameters, ISearchParameters
    {
        public string? SearchQuery { get; set; }
    }
}
