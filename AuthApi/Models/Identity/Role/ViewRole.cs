using LibUniversal.Models.Entities.UuidBaseEntity;

namespace AuthApi.Models.Identity.Role
{
    public class ViewRole : ViewUuidBaseEntity
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
