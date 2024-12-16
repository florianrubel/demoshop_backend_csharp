using Shared.Models;

namespace AuthApi.Models.Identity.Role
{
    public class ViewRole : UuidViewModel
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
