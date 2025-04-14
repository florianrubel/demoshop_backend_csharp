using LibUniversal.Models.Entities.RecursiveEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.LocalizedEntity
{
    public class ViewLocalizedEntity<EntityType> : ViewRecursiveEntity<EntityType>
    {
        [SwaggerParameter(LibUniversal.Entities.LocalizedEntity<EntityType>.DOC_LocaleId)]
        public Guid LocaleId { get; set; }

        [SwaggerParameter(LibUniversal.Entities.LocalizedEntity<EntityType>.DOC_LocalizationParentId)]
        public Guid? LocalizationParentId { get; set; }

        [SwaggerParameter(LibUniversal.Entities.LocalizedEntity<EntityType>.DOC_UseLocalizationFallback)]
        public bool UseLocalizationFallback { get; set; }
    }
}
