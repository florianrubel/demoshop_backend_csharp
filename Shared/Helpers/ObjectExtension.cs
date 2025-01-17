﻿using System.Dynamic;
using System.Reflection;

namespace Shared.Helpers
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
    }
}
