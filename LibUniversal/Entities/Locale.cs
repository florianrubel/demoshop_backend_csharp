using LibUniversal.Constants;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibUniversal.Entities
{
    public class Locale : UuidBaseEntity
    {
        public const string DOC_InternalLabel = "The internal label of the locale.";
        public const string DOC_LocaleCode = "The locale code of the locale. We recommend long ISO-Codes like en-US, de-DE, etc.";

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        [SwaggerParameter(DOC_InternalLabel)]
        public string? InternalLabel { get; set; }

        [MaxLength(InputSizes.LOCALE_CODE_LENGTH)]
        [SwaggerParameter(DOC_LocaleCode)]
        public string LocaleCode { get; set; }
    }
}
