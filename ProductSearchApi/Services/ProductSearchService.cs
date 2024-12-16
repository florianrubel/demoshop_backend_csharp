using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Microsoft.Extensions.Options;
using ProductSearchApi.Models;
using SharedProducts.Models.Search;

namespace ProductSearchApi.Services
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly IOptions<AlgoliaSettings> _algoliaSettings;

        public ProductSearchService(IOptions<AlgoliaSettings> algoliaSettings)
        {
            _algoliaSettings = algoliaSettings;
        }

        public async Task<SearchResponse<ProductSearchItem>> Search(ProductSearchRequest parameters)
        {
            var client = new SearchClient(_algoliaSettings.Value.ApplicationId, _algoliaSettings.Value.ReadApiKey);

            var filters = new List<string>();

            if (parameters.StringFacets != null)
            {
                foreach (var stringFacet in parameters.StringFacets)
                {
                    var values = stringFacet.Value;
                    var facetFilters = new List<string>();

                    if (values != null && values.Count > 0)
                    {
                        foreach (var value in values)
                        {
                            facetFilters.Add($"stringProperties.{stringFacet.Key}:{value}");
                        }
                    }

                    if (facetFilters.Count > 0)
                    {
                        filters.Add($"({String.Join(" OR ", facetFilters)})");
                    }
                }
            }

            if (parameters.BooleanFacets != null)
            {
                foreach (var booleanFacet in parameters.BooleanFacets)
                {
                    var value = booleanFacet.Value;
                    filters.Add($"booleanProperties.{booleanFacet.Key}:{value}");
                }
            }

            if (parameters.NumericFacets != null) {
                foreach (var numericFacet in parameters.NumericFacets)
                {
                    var rangeFilters = new List<string>();
                    var range = numericFacet.Value;
                    if (range.Min != null)
                    {
                        rangeFilters.Add($"numericProperties.{numericFacet.Key}>={range.Min}");
                    }
                    if (range.Max != null)
                    {
                        rangeFilters.Add($"numericProperties.{numericFacet.Key}<={range.Max}");
                    }
                    filters.Add($"({String.Join(" AND ", rangeFilters)})");
                }
            }

            var filterQuery = String.Join(" AND ", filters);

            return await client.SearchSingleIndexAsync<ProductSearchItem>(_algoliaSettings.Value.IndexName, new SearchParams(
                new SearchParamsObject
                {
                    Page = parameters.Page,
                    HitsPerPage = 25,
                    Distinct = new Distinct(parameters.Distinct == true),
                    Query = parameters.SearchQuery,
                    Filters = filterQuery,
                    Facets = new List<string>
                    {
                        "booleanProperties.*",
                        "numericProperties.*",
                        "stringProperties.*",
                        "priceInCents"
                    }
                }
            ));
        }
    }
}
