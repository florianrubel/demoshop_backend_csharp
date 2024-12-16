using System.Reflection;
using System.Linq.Dynamic.Core;

namespace Shared.Helpers
{
    public static class IQueryableExtension
    {
        public static string GetSortString<T>(this IQueryable<T> source, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "createdAt desc";

            string orderByString = "";
            string[] orderBySplit = orderBy.Split(",");

            foreach (string orderByClause in orderBySplit)
            {
                string trimmedOrderBy = orderByClause.Trim();
                bool orderDesc = trimmedOrderBy.EndsWith(" desc");
                int indexOfSpace = trimmedOrderBy.IndexOf(" ");
                string propertyName = indexOfSpace == -1 ? trimmedOrderBy : trimmedOrderBy.Remove(indexOfSpace);

                PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (propertyInfo == null)
                    continue;

                if (!string.IsNullOrWhiteSpace(orderByString))
                    orderByString = orderByString + ",";

                orderByString = orderByString + propertyName + (orderDesc ? " descending" : " ascending");
            }

            return orderByString;
        }
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy)
        {
            return source.OrderBy(GetSortString(source, orderBy));
        }
    }
}
