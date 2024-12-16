using Shared.Models.Api;

namespace StockApi.Models.Stock.StockItem
{
    public interface IStockItemPaginationParameters
    {
        string? ProductVariantIds { get; set; }
    }
}