using System;
using System.Linq;
using System.Linq.Expressions;

namespace Britespokes.Core.Extensions
{
  public static class QueryableExtensions
  {
    public static IQueryable<T> Paged<T>(this IQueryable<T> queryable, int page, int limit = 10, string sort = "Id")
      where T : Entity
    {
      if (page <= 0)
        page = 1;
      if (limit <= 0)
        limit = 10;

      return queryable.OrderBy(sort).Skip((page - 1)*limit).Take(limit);
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortExpression)
    {
      if (source == null)
        throw new ArgumentNullException("source", "source is null.");

      if (string.IsNullOrEmpty(sortExpression))
        throw new ArgumentException("sortExpression is null or empty.", "sortExpression");

      var parts = sortExpression.Split(' ');
      var isDescending = false;
      var tType = typeof(T);

      if (parts.Length > 0 && parts[0] != "")
      {
        var propertyName = parts[0];

        if (parts.Length > 1)
        {
          isDescending = parts[1].ToLower().Contains("esc");
        }

        var prop = tType.GetProperty(propertyName);

        if (prop == null)
        {
          throw new ArgumentException(string.Format("No property '{0}' on type '{1}'", propertyName, tType.Name));
        }

        var funcType = typeof(Func<,>)
            .MakeGenericType(tType, prop.PropertyType);

        var lambdaBuilder = typeof(Expression)
            .GetMethods()
            .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
            .MakeGenericMethod(funcType);

        var parameter = Expression.Parameter(tType);
        var propExpress = Expression.Property(parameter, prop);

        var sortLambda = lambdaBuilder
            .Invoke(null, new object[] { propExpress, new[] { parameter } });

// ReSharper disable PossibleNullReferenceException
        var sorter = typeof(Queryable)
            .GetMethods()
            .FirstOrDefault(x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
// ReSharper restore PossibleNullReferenceException
            .MakeGenericMethod(new[] { tType, prop.PropertyType });

        return (IQueryable<T>)sorter
            .Invoke(null, new[] { source, sortLambda });
      }

      return source;
    }
  }
}