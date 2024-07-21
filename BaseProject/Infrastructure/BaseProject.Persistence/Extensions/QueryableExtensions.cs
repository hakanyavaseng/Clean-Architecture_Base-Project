using BaseProject.Domain.Enums;
using BaseProject.Domain.Filtering;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace BaseProject.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFiltering<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            if (parameters.Filters != null && parameters.Filters.Any())
            {
                foreach (var filter in parameters.Filters)
                {
                    var propertyInfo = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null)
                    {
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var property = Expression.Property(parameter, propertyInfo);
                        var filterValue = filter.Value.Value;
                        var matchMode = filter.Value.MatchMode;

                        object convertedValue = null;
                        if (filterValue is JsonElement jsonElement)
                        {
                            if (propertyInfo.PropertyType == typeof(int))
                            {
                                convertedValue = jsonElement.GetInt32();
                            }
                            else if (propertyInfo.PropertyType == typeof(decimal))
                            {
                                convertedValue = jsonElement.GetDecimal();
                            }
                            else if (propertyInfo.PropertyType == typeof(double))
                            {
                                convertedValue = jsonElement.GetDouble();
                            }
                            else if (propertyInfo.PropertyType == typeof(float))
                            {
                                convertedValue = (float)jsonElement.GetDouble();
                            }
                            else if (propertyInfo.PropertyType == typeof(DateTime))
                            {
                                convertedValue = jsonElement.GetDateTime();
                            }
                            else if (propertyInfo.PropertyType == typeof(string))
                            {
                                convertedValue = jsonElement.GetString();
                            }
                            else
                            {
                                convertedValue = jsonElement.Deserialize(propertyInfo.PropertyType);
                            }
                        }
                        else
                        {
                            convertedValue = Convert.ChangeType(filterValue, propertyInfo.PropertyType);
                        }

                        var constant = Expression.Constant(convertedValue, propertyInfo.PropertyType);

                        Expression comparison = matchMode switch
                        {
                            MatchMode.Equals => Expression.Equal(property, constant),
                            MatchMode.Contains => Expression.Call(property, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                            MatchMode.StartsWith => Expression.Call(property, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant),
                            MatchMode.EndsWith => Expression.Call(property, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant),
                            MatchMode.GreaterThan => Expression.GreaterThan(property, constant),
                            MatchMode.LessThan => Expression.LessThan(property, constant),
                            _ => throw new ArgumentOutOfRangeException()
                        };

                        var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                        query = query.Where(lambda);
                    }
                }
            }

            return query;
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.SortColumn))
            {
                var propertyInfo = typeof(T).GetProperty(parameters.SortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, propertyInfo);
                    var keySelector = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

                    query = parameters.SortDescending
                        ? query.OrderByDescending(keySelector)
                        : query.OrderBy(keySelector);
                }
            }

            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            return query.Skip((parameters.PageNumber) * parameters.PageSize)
                        .Take(parameters.PageSize);
        }
    }
}
