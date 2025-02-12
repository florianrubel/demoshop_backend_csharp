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

        public Dictionary<string, List<string>>? StringFilters { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, NumericRange>? NumericFilters { get; set; } = new Dictionary<string, NumericRange>();

        public Dictionary<string, List<bool>>? BooleanFilters { get; set; } = new Dictionary<string, List<bool>>();
    }
}
