using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models.Authentication
{
    public class SignInUser
    {
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string UserName { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
