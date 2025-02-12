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

            if (parameters.StringFilters != null)
            {
                foreach (var stringFacet in parameters.StringFilters)
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

            if (parameters.BooleanFilters != null)
            {
                foreach (var booleanFacet in parameters.BooleanFilters)
                {
                    var values = booleanFacet.Value;
                    var facetFilters = new List<string>();

                    if (values != null && values.Count > 0)
                    {
                        foreach (var value in values)
                        {
                            var key = booleanFacet.Key;
                            var strValue = value.ToString().ToLower();
                            facetFilters.Add($"booleanProperties.{key}:{strValue}");
                        }
                    }

                    if (facetFilters.Count > 0)
                    {
                        filters.Add($"({String.Join(" OR ", facetFilters)})");
                    }
                }
            }

            if (parameters.NumericFilters != null) {
                foreach (var numericFacet in parameters.NumericFilters)
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
                    if (rangeFilters.Count > 0) {
                        filters.Add($"({String.Join(" AND ", rangeFilters)})");
                    }
                }
            }

            var filterQuery = String.Join(" AND ", filters);

            Console.WriteLine(parameters.Page);

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
