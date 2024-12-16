using Microsoft.AspNetCore.Identity;

namespace AuthApi.Entities.Identity
{
    public class Role : IdentityRole
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
