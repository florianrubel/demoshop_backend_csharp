using LibUniversal.Constants;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibContent.Models.ContentElement
{
    public class FieldConfig
    {
        public const string DOC_Label = "The label for the property.";
        public const string DOC_DataType = "The data type for the property.";

        [SwaggerParameter(DOC_Label)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Label { get; set; }

        [SwaggerParameter(DOC_DataType)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string DataType { get; set; }
    }
}
