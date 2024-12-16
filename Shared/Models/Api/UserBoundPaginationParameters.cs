namespace Shared.Models.Api
{
    public class UserBoundPaginationParameters : PaginationParameters
    {
        public string? UserIds { get; set; }
    }
}
