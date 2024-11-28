using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Api
{
    public class PaginationParameters : ShapingWithOrderingParameters, IPaginationParameters
    {
        [Range(1, double.PositiveInfinity)]
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 25;
    }
}
