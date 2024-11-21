namespace Shared.Models.Api
{
    public interface IPaginationParameters : IShapingWithOrderingParameters
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}