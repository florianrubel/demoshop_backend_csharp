using Shared.Entities;
using SharedProducts.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockApi.Entities.Stock
{
    public class StockItem : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }

        public DateTimeOffset? ReservedAt { get; set; }

        public DateTimeOffset? SoldAt { get; set; }
    }
}
