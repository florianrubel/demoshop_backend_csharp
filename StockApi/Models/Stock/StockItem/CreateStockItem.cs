using System.ComponentModel.DataAnnotations;

namespace StockApi.Models.Stock.StockItem
{
    public class CreateStockItem
    {
        [Required]
        public Guid? ProductVariantId { get; set; }
    }
}
