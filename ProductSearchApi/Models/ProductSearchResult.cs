using SharedProducts.Models.Search;

namespace ProductSearchApi.Models
{
    public class ProductSearchResult
    {
        public Dictionary<string, Dictionary<string, int>> BooleanFacets { get; set; } = new Dictionary<string, Dictionary<string, int>>();

        public Dictionary<string, Dictionary<string, int>> NumericFacets { get; set; } = new Dictionary<string, Dictionary<string, int>>();

        public Dictionary<string, Dictionary<string, int>> StringFacets { get; set; } = new Dictionary<string, Dictionary<string, int>>();

        public NumericRange PriceInCents { get; set; } = new NumericRange();

        public List<ProductSearchItem> Products { get; set; }
    }
}
