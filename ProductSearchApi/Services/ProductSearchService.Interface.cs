using Algolia.Search.Models.Search;
using ProductSearchApi.Models;
using SharedProducts.Models.Search;

namespace ProductSearchApi.Services
{
    public interface IProductSearchService
    {
        Task<SearchResponse<ProductSearchItem>> Search(ProductSearchRequest paramemters);
    }
}