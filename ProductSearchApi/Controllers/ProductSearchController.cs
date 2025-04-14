using Algolia.Search.Models.QuerySuggestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSearchApi.Models;
using ProductSearchApi.Services;
using Shared.Helpers;

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
            Console.WriteLine(result.Page);
            SetPaginationHeaders(
                result.NbHits ?? 0,
                result.HitsPerPage ?? 0,
                result.Page ?? 0,
                result.NbPages ?? 0
            );

            var searchResult = new ProductSearchResult
            {
                Products = result.Hits,
                BooleanFilters = parameters.BooleanFilters,
                NumericFilters = parameters.NumericFilters,
                StringFilters = parameters.StringFilters,
            };
            var priceRange = result.FacetsStats != null ? result.FacetsStats["priceInCents"] ?? null : null;

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

            foreach (var facetStat in result.FacetsStats)
            {
                if (facetStat.Key.StartsWith("numericProperties."))
                {
                    var property = facetStat.Key.Replace("numericProperties.", "");
                    searchResult.NumericFacetsRanges.Add(property, new NumericRange
                    {
                        Min =  Convert.ToInt32(facetStat.Value.Min),
                        Max = Convert.ToInt32(facetStat.Value.Max),
                    });
                }
            }

            foreach(var stringFilter in parameters.StringFilters)
            {
                var sRequest = parameters.Clone();
                sRequest.StringFilters.Remove(stringFilter.Key);
                var sResult = await _productSearchService.Search(sRequest);

                foreach(var facet in sResult.Facets)
                {
                    var key = facet.Key.Replace("stringProperties.", "");
                    if (stringFilter.Key == key)
                    {
                        if (searchResult.StringFacets.ContainsKey(key))
                        {
                            searchResult.StringFacets[key] = facet.Value;
                        }
                        else
                        {
                            searchResult.StringFacets.Add(key, facet.Value);
                        }
                    }
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
