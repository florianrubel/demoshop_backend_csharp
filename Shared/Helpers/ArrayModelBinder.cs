using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace Shared.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            string value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            Type elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments.Length > 0
                ? bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0]
                : typeof(string);
            TypeConverter converter = TypeDescriptor.GetConverter(elementType);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            object[] values = value.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
                .Distinct()
                .Select(x => converter.ConvertFromString(x.Trim()))
                .Distinct()
                .ToArray();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            Array typeValues = Array.CreateInstance(elementType, values.Length);

            values.CopyTo(typeValues, 0);
            bindingContext.Model = typeValues;

            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
