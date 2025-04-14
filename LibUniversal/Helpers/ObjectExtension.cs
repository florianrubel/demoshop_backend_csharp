using System.Dynamic;
using System.Reflection;
using System.Text.Json;

namespace LibUniversal.Helpers
{
    public static class ObjectExtension
    {
        public static ExpandoObject ShapeData<TSource>(this TSource source, string? fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            ExpandoObject dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                PropertyInfo[] propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    object? propertyValue = propertyInfo.GetValue(source);

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8604 // Possible null reference argument.
                    // alternative way
                    // dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
                }
                return dataShapedObject;
            }
            string[] fieldsAfterSplit = fields.Split(",");
            foreach (string field in fieldsAfterSplit)
            {
                string propertyName = field.Trim();
                PropertyInfo? propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (propertyInfo == null)
                    continue;

                object? propertyValue = propertyInfo.GetValue(source);

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8604 // Possible null reference argument.
                // alternative way
                // dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
            }

            return dataShapedObject;
        }
        public static void TrimStringProperties<TSource>(this TSource obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // Get all properties of the object
            var properties = obj.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                // Get the current value of the property
                var currentValue = property.GetValue(obj) as string;

                if (currentValue != null)
                {
                    // Trim the value and set it back
                    property.SetValue(obj, currentValue.Trim());
                }
            }
        }
        public static TSource Clone<TSource>(this TSource obj)
        {
            return JsonSerializer.Deserialize<TSource>(JsonSerializer.Serialize(obj));
        }
    }
}
