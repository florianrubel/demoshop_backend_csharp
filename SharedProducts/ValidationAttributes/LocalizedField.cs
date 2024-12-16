using System.ComponentModel.DataAnnotations;

namespace SharedProducts.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class LocalizedField : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is Dictionary<string, string>))
                return new ValidationResult("Object is not of proper type");

            var dictionary = (Dictionary<string, string>)value;

            if (dictionary.Count > 100)
                return new ValidationResult("Dictionary cant have more than 10 items");

            foreach (var keyValuePair in dictionary)
            {
                if (keyValuePair.Value.Length > 2000)
                    return new ValidationResult("Value cant be longer than 2000 chars.");
            }

            return ValidationResult.Success;
        }
    }
}
