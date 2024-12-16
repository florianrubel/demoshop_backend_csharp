using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSearchApi.Models;
using ProductSearchApi.Services;

namespace ProductSearchApi.Controllers
{
    [ApiController]
    [Route("product-search")]
    [AllowAnonymous]
    public class ProductSearchController : ControllerBase
    {
        private readonly IProductSearchService _productSearchService;

        public ProductSearchController(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductSearchResult>> Search([FromBody] ProductSearchRequest parameters)
        {
            var result = await _productSearchService.Search(parameters);
            SetPaginationHeaders(
                result.NbHits ?? 0,
                result.HitsPerPage ?? 0,
                result.Page ?? 0,
                result.NbPages ?? 0
            );

            var searchResult = new ProductSearchResult
            {
                Products = result.Hits
            };
            var priceRange = result.FacetsStats["priceInCents"];

            foreach (var facet in result.Facets)
            {
                if (facet.Key.StartsWith("booleanProperties"))
                {
                    searchResult.BooleanFacets.Add(facet.Key.Replace("booleanProperties.", ""), facet.Value);
                }
                if (facet.Key.StartsWith("numericProperties"))
                {
                    searchResult.NumericFacets.Add(facet.Key.Replace("numericProperties.", ""), facet.Value);
                }
                if (facet.Key.StartsWith("stringProperties"))
                {
                    searchResult.StringFacets.Add(facet.Key.Replace("stringProperties.", ""), facet.Value);
                }
            }

            if (priceRange != null)
            {
                searchResult.PriceInCents.Min = Convert.ToInt32(priceRange.Min);
                searchResult.PriceInCents.Max = Convert.ToInt32(priceRange.Max);
            }

            return Ok(searchResult);
        }

        protected virtual void SetPaginationHeaders(int totalCount, int pageSize, int page, int totalPages)
        {
            Response.Headers.Append("Pagination.TotalCount", totalCount.ToString());
            Response.Headers.Append("Pagination.PageSize", pageSize.ToString());
            Response.Headers.Append("Pagination.Page", page.ToString());
            Response.Headers.Append("Pagination.TotalPages", totalPages.ToString());
        }
    }
}
