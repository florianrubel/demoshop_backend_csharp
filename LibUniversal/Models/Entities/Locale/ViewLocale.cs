using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.Locale
{
    public class ViewLocale : ViewUuidBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.Locale.DOC_InternalLabel)]
        public string InternalLabel { get; set; }

        [SwaggerParameter(LibUniversal.Entities.Locale.DOC_LocaleCode)]
        public string LocaleCode { get; set; }
    }
}
