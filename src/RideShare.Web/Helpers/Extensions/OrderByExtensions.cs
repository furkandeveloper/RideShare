using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Extensions
{
    public static class OrderByExtensions
    {
        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string fieldName, string orderMethodName = "OrderBy")
        {
            var parameter = Expression.Parameter(typeof(T), "a");
            var prop = Expression.Property(parameter, fieldName);
            var lambda = Expression.Lambda(prop, parameter);
            var method = typeof(Queryable).GetMethods().FirstOrDefault(x => x.Name == orderMethodName).MakeGenericMethod(typeof(T), prop.Type);
            return method.Invoke(null, new object[] { source, lambda }) as IOrderedQueryable<T>;
        }

        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, Enum fieldName, Enum orderMethodName)
        {
            return source.ApplyOrder(fieldName.ToString(), orderMethodName.ToString());
        }
    }
}
