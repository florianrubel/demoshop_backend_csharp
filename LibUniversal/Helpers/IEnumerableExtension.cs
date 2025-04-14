using System.Dynamic;
using System.Reflection;

namespace LibUniversal.Helpers
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            List<ExpandoObject> objectList = new List<ExpandoObject>();
            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                PropertyInfo[] propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                string[] fieldsAfterSplit = fields.Split(",");
                foreach (string field in fieldsAfterSplit)
                {
                    string propertyName = field.Trim();
                    PropertyInfo? propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (propertyInfo == null)
                        continue;

                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource sourceObject in source)
            {
                ExpandoObject dataShapedObject = new ExpandoObject();
                foreach (PropertyInfo propertyInfo in propertyInfoList)
                {
                    object? propertyValue = propertyInfo.GetValue(sourceObject);

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                    ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propertyValue);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8604 // Possible null reference argument.
                    // alternative way
                    // dataShapedObject.TryAdd(propertyInfo.Name, propertyValue);
                }

                objectList.Add(dataShapedObject);
            }

            return objectList;
        }

        public static void TrimStringProperties<TSource>(this IEnumerable<TSource> source)
        {
            foreach (TSource obj in source)
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
        }
    }
}
