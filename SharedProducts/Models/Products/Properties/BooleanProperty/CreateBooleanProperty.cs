using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.Properties.BooleanProperty
{
    public class CreateBooleanProperty
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
