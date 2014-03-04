using System;
using System.Linq;
using System.Linq.Expressions;

namespace Britespokes.Core.Data
{
  /// <summary>
  /// Repository
  /// </summary>
  public interface IRepository<T> where T : BaseEntity
  {
    T Find(object id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(object id);
    IQueryable<T> All { get; }
    IQueryable<T> FindBy(Expression<Func<T, bool>> filter,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        string includeProperties = "");
    IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
  }
}