using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqLearn.Helpers
{
    public static class ExpressionCombiner
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression,
            Expression<Func<T, bool>> newExpression)
        {

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(expression.Body,
                Expression.Invoke(newExpression, expression.Parameters)),
                expression.Parameters
                );
        }
    }
}
