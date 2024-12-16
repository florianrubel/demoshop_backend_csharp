using Shared.Repositories;
using StockApi.Entities.Stock;
using StockApi.Models.Stock.StockItem;

namespace StockApi.Repositories.Stock
{
    public interface IStockItemRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : StockItem
        where SearchParametersType : StockItemPaginationParameters
    {
    }
}