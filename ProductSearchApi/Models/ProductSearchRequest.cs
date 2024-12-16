using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace ProductSearchApi.Models
{
    public class ProductSearchRequest
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; } = 1;

        public bool? Distinct { get; set; } = true;

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? SearchQuery { get; set; }

        public Dictionary<string, List<string>>? StringFacets { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, NumericRange>? NumericFacets { get; set; } = new Dictionary<string, NumericRange>();

        public Dictionary<string, bool>? BooleanFacets { get; set; } = new Dictionary<string, bool>();
    }
}
