using Heyday.Application.Catalog.Contracts;
using Heyday.Domain.Contracts;
using Heyday.Infrastructure.Conventers;
using Heyday.Shared.Filters;

using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heyday.Infrastructure.Extensions.SubExtensions;

public static class QueryableExtensions
{
    //public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec)
    //where T : BaseEntity
    //{
    //    var queryableResultWithIncludes = spec.Includes
    //        .Aggregate(query, (current, include) => current.Include(include));
    //    var secondaryResult = spec.IncludeStrings
    //        .Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));
    //    if (spec.Criteria == null)
    //        return secondaryResult;
    //    else
    //        return secondaryResult.Where(spec.Criteria);
    //}

    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Filters<T>? filters)
    {
        if (filters?.IsValid() == true)
            query = filters.Get().Aggregate(query, (current, filter) => current.Where(filter.Expression));
        return query;
    }

    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string[]? orderBy)
    where T : BaseEntity
    {
        string? ordering = new OrderByConverter().ConvertBack(orderBy);
        return !string.IsNullOrWhiteSpace(ordering) ? query.OrderBy(ordering) : query.OrderBy(a => a.id);
    }

    public static Expression<Func<T, bool>> False<T>()
    {
        return _ => false;
    }

    public static IQueryable<T> AdvancedSearch<T>(this IQueryable<T> query, Search search)
    {
        var predicate = False<T>();
        foreach (var propertyInfo in typeof(T).GetProperties()
            .Where(p => p.GetGetMethod()?.IsVirtual is false &&
                        search.Fields.Any(field => p.Name.Equals(field, StringComparison.OrdinalIgnoreCase))))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var nullCheck = Expression.NotEqual(propertyAsObject, Expression.Constant(null, typeof(object)));
            var propertyAsString = Expression.Call(property, "ToString", null, null);
            var keywordExpression = Expression.Constant(search.Keyword);
            var contains = propertyInfo.PropertyType == typeof(string) ? Expression.Call(property, "Contains", null, keywordExpression) : Expression.Call(propertyAsString, "Contains", null, keywordExpression);
            var lambda = Expression.Lambda(Expression.AndAlso(nullCheck, contains), parameter);
            predicate = predicate.Or((Expression<Func<T, bool>>)lambda);
        }

        return query.Where(predicate);
    }

    private static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }

    private static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }


    public static IQueryable<T> SearchByKeyword<T>(this IQueryable<T> query, string keyword)
    {
        var predicate = False<T>();
        var properties = typeof(T).GetProperties();
        foreach (var propertyInfo in properties.Where(p => p.GetGetMethod()?.IsVirtual is false))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var propertyAsObject = Expression.Convert(property, typeof(object));
            var nullCheck = Expression.NotEqual(propertyAsObject, Expression.Constant(null, typeof(object)));
            var propertyAsString = Expression.Call(property, "ToString", null, null);
            var keywordExpression = Expression.Constant(keyword);
            var contains = propertyInfo.PropertyType == typeof(string) ? Expression.Call(property, "Contains", null, keywordExpression) : Expression.Call(propertyAsString, "Contains", null, keywordExpression);
            var lambda = Expression.Lambda(Expression.AndAlso(nullCheck, contains), parameter);
            predicate = predicate.Or((Expression<Func<T, bool>>)lambda);
        }

        return query.Where(predicate);
    }

}
