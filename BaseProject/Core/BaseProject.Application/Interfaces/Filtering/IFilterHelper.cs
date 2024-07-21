using BaseProject.Domain.Filtering;
using System.Linq.Expressions;
using System.Reflection;

namespace BaseProject.Application.Interfaces.Filtering
{
    public interface IFilterHelper
    {
        static abstract Expression<Func<T, bool>> GetExpression<T>(List<QueryFilter> filters);
    }
    public class FilterHelper : IFilterHelper
    {
        public static Expression<Func<T, bool>> GetExpression<T>(List<QueryFilter> filters)
        {
            if (filters == null || filters.Count == 0)
                return x => true;

            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression exp = null;

            foreach (var filter in filters)
            {
                Expression expPart = null;
                var property = typeof(T).GetProperty(filter.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                    throw new Exception($"Property '{filter.PropertyName}' not found on type '{typeof(T).Name}'");

                var left = Expression.Property(param, property);
                var right = Expression.Constant(Convert.ChangeType(filter.Value, property.PropertyType));

                switch (filter.Operation.ToLower())
                {
                    case "equals":
                        expPart = Expression.Equal(left, right);
                        break;
                    case "notequals":
                        expPart = Expression.NotEqual(left, right);
                        break;
                    case "contains":
                        expPart = Expression.Call(left, "Contains", null, right);
                        break;
                    case "greaterthan":
                        expPart = Expression.GreaterThan(left, right);
                        break;
                    case "lessthan":
                        expPart = Expression.LessThan(left, right);
                        break;
                }

                if (expPart == null)
                    throw new Exception($"Operation '{filter.Operation}' is not supported");

                if (exp == null)
                {
                    exp = expPart;
                }
                else
                {
                    exp = Expression.AndAlso(exp, expPart);
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
    }
}
