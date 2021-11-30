using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Inflow.Modules.Wallets.Application.Wallets.Storage;

internal static class Extensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        var firstParam = a.Parameters[0];
        var visitor = new SubstExpressionVisitor
        {
            Dictionary =
            {
                [b.Parameters[0]] = firstParam
            }
        };

        var body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, firstParam);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
    {
        var firstParam = a.Parameters[0];
        var visitor = new SubstExpressionVisitor
        {
            Dictionary =
            {
                [b.Parameters[0]] = firstParam
            }
        };

        var body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
        return Expression.Lambda<Func<T, bool>>(body, firstParam);
    }


    private class SubstExpressionVisitor : ExpressionVisitor
    {
        public readonly Dictionary<Expression, Expression> Dictionary = new();

        protected override Expression VisitParameter(ParameterExpression node)
            => Dictionary.TryGetValue(node, out var newValue) ? newValue : node;
    }
}