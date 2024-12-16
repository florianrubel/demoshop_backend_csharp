using System.ComponentModel.DataAnnotations;

namespace ProductSearchApi.Models
{
    public class NumericRange
    {
        [Range(1, int.MaxValue)]
        public int? Min { get; set; }

        [Range(1, int.MaxValue)]
        public int? Max { get; set; }
    }
}
