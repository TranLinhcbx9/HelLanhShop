using HelLanhShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Helpers
{
    public static class FilterEngine
    {
        public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, List<FilterRule>? rules)
        {
            if (rules == null || rules.Count == 0) return query;

            var parameter = Expression.Parameter(typeof(T), "x");

            Expression? finalExpr = null;

            foreach (var rule in rules)
            {
                var property = typeof(T).GetProperty(rule.Field);
                if (property == null) continue;

                var left = Expression.Property(parameter, property);

                Expression? condition = rule.Operator.ToLower() switch
                {
                    "contains" => BuildContains(left, rule.Value),
                    "equals" => BuildEquals(left, rule.Value),
                    "starts" => BuildStarts(left, rule.Value),
                    "ends" => BuildEnds(left, rule.Value),
                    "gt" => BuildCompare(left, rule.Value, ExpressionType.GreaterThan),
                    "gte" => BuildCompare(left, rule.Value, ExpressionType.GreaterThanOrEqual),
                    "lt" => BuildCompare(left, rule.Value, ExpressionType.LessThan),
                    "lte" => BuildCompare(left, rule.Value, ExpressionType.LessThanOrEqual),
                    "between" => BuildBetween(left, rule.Value, rule.Value2),
                    _ => null
                };

                if (condition == null) continue;

                finalExpr = finalExpr == null ? condition : Expression.AndAlso(finalExpr, condition);
            }

            if (finalExpr == null) return query;

            var lambda = Expression.Lambda<Func<T, bool>>(finalExpr, parameter);
            return query.Where(lambda);
        }

        private static Expression BuildContains(Expression left, string? value)
        {
            return Expression.Call(left, nameof(string.Contains), null, Expression.Constant(value));
        }

        private static Expression BuildStarts(Expression left, string? value)
        {
            return Expression.Call(left, nameof(string.StartsWith), null, Expression.Constant(value));     
        }

        private static Expression BuildEnds(Expression left, string? value)
        {
            return Expression.Call(left, nameof(string.EndsWith), null, Expression.Constant(value));
        }

        private static Expression BuildEquals(Expression left, string? value)
        {
            var typedValue = Convert.ChangeType(value, left.Type);
            return Expression.Equal(left, Expression.Constant(typedValue));
        }

        private static Expression BuildCompare(Expression left, string? value, ExpressionType op)
        {
            var typedValue = Convert.ChangeType(value, left.Type);
            //var converted = Expression.Convert(Expression.Constant(typedValue), left.Type);
            return Expression.MakeBinary(op, left, Expression.Constant(typedValue));
        }

        private static Expression BuildBetween(Expression left, string? v1, string? v2)
        {
            var typedV1 = Convert.ChangeType(v1, left.Type);
            var typedV2 = Convert.ChangeType(v2, left.Type);

            var gte = Expression.GreaterThanOrEqual(left, Expression.Constant(typedV1));

            var lte = Expression.LessThanOrEqual(left, Expression.Constant(typedV2));

            return Expression.AndAlso(gte, lte);
        }
    }
}
