using LibUniversal.Constants;
using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibUniversal.Models.Entities.Locale
{
    public class CreateLocale : CreateUuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        [SwaggerParameter(LibUniversal.Entities.Locale.DOC_InternalLabel)]
        public string? InternalLabel { get; set; }

        [MaxLength(InputSizes.LOCALE_CODE_LENGTH)]
        [MinLength(InputSizes.COUNTRY_CODE_LENGTH)]
        [SwaggerParameter(LibUniversal.Entities.Locale.DOC_LocaleCode)]
        public string LocaleCode { get; set; }
    }
}
