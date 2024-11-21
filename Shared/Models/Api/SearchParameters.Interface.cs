namespace Shared.Models.Api
{
    public interface ISearchParameters : IPaginationParameters
    {
        string? SearchQuery { get; set; }
    }
}