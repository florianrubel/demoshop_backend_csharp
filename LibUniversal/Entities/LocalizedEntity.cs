using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibUniversal.Entities
{
    public class LocalizedEntity<ParentType> : RecursiveEntity<ParentType>
    {
        public const string DOC_LocaleId = "The id of the locale.";
        public const string DOC_Locale = "The lazy loaded locale object";
        public const string DOC_LocalizationParentId = "The id of the base language entity.";
        public const string DOC_LocalizationParent = "The lazy loaded object of the base language entity.";
        public const string DOC_LocalizedChildren = "The lazy loaded list of the localized version of the base entity.";
        public const string DOC_UseLocalizationFallback = "If a localized entity in a localized version is missing and this option is set to true, the entity with the default language will be shown.";

        [ForeignKey(nameof(LocaleId))]
        [SwaggerParameter(DOC_LocaleId)]
        public Guid LocaleId { get; set; }
        [SwaggerParameter(DOC_Locale)]
        public virtual Locale Locale { get; set; }

        [SwaggerParameter(DOC_LocalizationParentId)]
        [ForeignKey(nameof(LocalizationParentId))]
        public Guid? LocalizationParentId { get; set; }
        [SwaggerParameter(DOC_LocalizationParent)]
        public virtual ParentType? LocalizationParent { get; set; }
        [SwaggerParameter(DOC_LocalizedChildren)]
        public virtual List<ParentType> LocalizedChildren { get; set; } = new List<ParentType>();

        [SwaggerParameter(DOC_UseLocalizationFallback)]
        public bool UseLocalizationFallback { get; set; }
    }
}
