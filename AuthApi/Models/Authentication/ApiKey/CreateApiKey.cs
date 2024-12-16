using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models.Authentication
{
    public class CreateApiKey : ICreateApiKey
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Scope { get; set; }

        public List<string>? Roles { get; set; } = new List<string>();
    }
}
