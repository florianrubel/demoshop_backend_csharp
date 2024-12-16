using Shared.Models.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace StockApi.Models.Stock.StockItem
{
    public class StockItemPaginationParameters : PaginationParameters
    {
        [SwaggerParameter("Comma separated list of guids")]
        public string? ProductVariantIds { get; set; }

        public bool? IsAvailable { get; set; }
    }
}
