namespace Shared.Models.Api
{
    public class UserBoundSearchParameters : UserBoundPaginationParameters
    {
        public string? SearchQuery { get; set; }
    }
}
