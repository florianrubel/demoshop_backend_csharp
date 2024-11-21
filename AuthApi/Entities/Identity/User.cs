using Microsoft.AspNetCore.Identity;
using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Entities.Identity
{
    public class User : IdentityUser
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? FirstName { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? LastName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
