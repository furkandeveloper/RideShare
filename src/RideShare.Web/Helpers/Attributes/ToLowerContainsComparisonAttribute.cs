using AutoFilterer.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RideShare.Web.Helpers.Attributes
{
    public class ToLowerContainsComparisonAttribute : FilteringOptionsBaseAttribute
    {
        public override Expression BuildExpression(Expression expressionBody, PropertyInfo targetProperty, PropertyInfo filterProperty, object value)
        {
            var containsMethod = typeof(string).GetMethod(nameof(string.Contains), types: new[] { typeof(string) });

            var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), types: new Type[0]);

            var comparison = Expression.Equal(
                        Expression.Call(
                            method: containsMethod,
                            instance: Expression.Call(method: toLowerMethod, instance: Expression.Property(expressionBody, filterProperty.Name)
                                ),
                            arguments: new[] { Expression.Call(method: toLowerMethod, instance: Expression.Constant(value)) }),
                        Expression.Constant(true));

            return comparison;
        }
    }
}
