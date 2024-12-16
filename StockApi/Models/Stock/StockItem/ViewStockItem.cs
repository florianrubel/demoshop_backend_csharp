using Shared.Models;

namespace StockApi.Models.Stock.StockItem
{
    public class ViewStockItem : UuidViewModel
    {
        public Guid ProductVariantId { get; set; }

        public DateTimeOffset? ReservedAt { get; set; }

        public DateTimeOffset? SoldAt { get; set; }
    }
}
