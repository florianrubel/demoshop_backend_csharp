using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models.Api;
using StockApi.Entities.Stock;
using StockApi.Models.Stock.StockItem;
using StockApi.Repositories.Stock;
using Swashbuckle.AspNetCore.Annotations;

namespace StockApi.Controllers.Stock
{
    [Route("stock/stock-item")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class StockItemController : DefaultControllerTemplate<StockItem, ViewStockItem, CreateStockItem, PatchStockItem, StockItemPaginationParameters>
    {
        public StockItemController(IMapper mapper, IStockItemRepository<StockItem, StockItemPaginationParameters> repository) : base(mapper, repository)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<ActionResult<Dictionary<Guid, ViewStockItem>>> Patch([FromBody] Dictionary<Guid, JsonPatchDocument<PatchStockItem>> patchDocuments)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("reserve/{productVariantId}")]
        [SwaggerOperation(
            "Reserve stock item",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Reserves the first available stock item for the provided product variant it.</p>"

        )]
        public async Task<ActionResult<ViewStockItem>> ReserveStock(Guid productVariantId)
        {
            var stockItem = (await _repository.GetMultiple(new StockItemPaginationParameters
            {
                PageSize = 1,
                IsAvailable = true,
                ProductVariantIds = productVariantId.ToString()
            })).FirstOrDefault();

            if (stockItem == null)
            {
                return BadRequest(new ApiError
                {
                    ErrorCode = "not_available",
                    Details = "There is no free stock item for this product variant."
                });
            }

            stockItem.ReservedAt = DateTimeOffset.Now;
            await _repository.Update(stockItem);

            return Ok(stockItem);
        }

        [HttpPost]
        [Route("sell/{id}")]
        [SwaggerOperation(
            "Sell stock item",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Marks a stock item as sold.</p>"

        )]
        public async Task<ActionResult<ViewStockItem>> SellStock(Guid id)
        {
            var stockItem = await _repository.GetOneOrDefault(id);

            if (stockItem == null)
            {
                return BadRequest();
            }

            if (stockItem.SoldAt != null)
            {
                return BadRequest(new ApiError
                {
                    ErrorCode = "stock_sold",
                    Details = "The stock item has already been sold."
                });
            }

            stockItem.SoldAt = DateTimeOffset.Now;
            await _repository.Update(stockItem);

            return Ok(stockItem);
        }

        [HttpPost]
        [Route("seed")]
        [SwaggerOperation(
            "Seed stock items",
            "<hr/><p><strong>&#x1F510; Authentication: Bearer</p><p><hr/></hr>" +
            "<p>Seeding stock items on startup is not possible, so it has to be done after everything else is running.</p>" +
            "<p>Seed stock items for the provided product variant ids." +
            "<pre><code>" +
            "[\n" +
                "\"e0f81c5b-9d06-475e-8691-92aa6ccb166f\"\n" +
                "\"a156c451-6375-4040-9d1c-b79a8f230b1b\"\n" +
            "]" +
            "</code></pre>"

        )]
        public async Task<ActionResult> Seed(IEnumerable<Guid> productVariantIds)
        {
            var stockItems = new List<StockItem>();

            var random = new Random();

            foreach (var id in productVariantIds)
            {
                for (var i = 0; i < random.Next(0, 10); i++)
                {
                    stockItems.Add(new StockItem
                    {
                        ProductVariantId = id,
                    });
                }
            }

            await _repository.CreateRange(stockItems);

            return Ok();
        }
    }
}
