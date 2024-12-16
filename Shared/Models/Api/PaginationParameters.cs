using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models.Api
{
    public class PaginationParameters : ShapingWithOrderingParameters
    {
        [SwaggerParameter("The page you want to receive. Must be larger than 0.")]
        [Range(1, double.PositiveInfinity)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// -1 for unlimited
        /// </summary>
        [SwaggerParameter("Amount of records per page (-1 for unlimited, default: 25).")]
        [Range(-1, double.PositiveInfinity)]
        public int PageSize { get; set; } = 25;
    }
}
