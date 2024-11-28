using Shared.Models;

namespace AuthApi.Models.Identity.User
{
    public class ViewUser : UuidViewModel
    {
        public string? UserName { get; set; }

        public string? NormalizedUserName { get; set; }

        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string? FirstName { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public string? LastName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
