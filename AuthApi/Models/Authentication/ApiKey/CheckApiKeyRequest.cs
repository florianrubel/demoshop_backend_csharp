using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models.Authentication.ApiKey
{
    public class CheckApiKeyRequest : ICheckApiKeyRequest
    {
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string ApiKey { get; set; }
    }
}
