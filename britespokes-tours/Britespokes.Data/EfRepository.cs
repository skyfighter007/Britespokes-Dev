using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Britespokes.Core;
using Britespokes.Core.Data;
using System.Data;

namespace Britespokes.Data
{
  public class EfRepository<T> : IRepository<T> where T : BaseEntity
  {
    private readonly EfDbContext _context;
    private IDbSet<T> _entities;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="context">Object context</param>
    public EfRepository(EfDbContext context)
    {
      _context = context;
    }

    public T Find(object id)
    {
      return Entities.Find(id);
    }

    public void Add(T entity)
    {
      try
      {
        if (entity == null)
          throw new ArgumentNullException("entity");

        Entities.Add(entity);

        _context.Save();
      }
      catch (DbEntityValidationException dbEx)
      {
        throw WrappedDbEntityValidationException(dbEx);
      }
    }

    public void Update(T entity)
    {
      try
      {
        if (entity == null)
          throw new ArgumentNullException("entity");

        _context.Entry(entity).State = EntityState.Modified;
        _context.Save();
      }
      catch (DbEntityValidationException dbEx)
      {
        throw WrappedDbEntityValidationException(dbEx);
      }
    }

    public void Delete(object id)
    {
      Delete(Find(id));
    }

    public void Delete(T entity)
    {
      try
      {
        if (entity == null)
          throw new ArgumentNullException("entity");

        Entities.Remove(entity);

        _context.Save();
      }
      catch (DbEntityValidationException dbEx)
      {
        throw WrappedDbEntityValidationException(dbEx);
      }
    }

    public virtual IQueryable<T> All
    {
      get
      {
        return Entities;
      }
    }

    private IDbSet<T> Entities
    {
      get { return _entities ?? (_entities = _context.Set<T>()); }
    }

    public IQueryable<T> FindBy(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties = "")
    {
      var query = All;

      if (filter != null)
        query = query.Where(filter);

      query = includeProperties.
        Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).
        Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

      return orderBy != null ? orderBy(query) : query;
    }

    public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
    {
      var query = All.Where(predicate);
      return query;
    }

    private Exception WrappedDbEntityValidationException(DbEntityValidationException dbEx)
    {
      var msg = string.Empty;
      const string msgFmt = "Property: {0} Error: {1}";
      var sep = Environment.NewLine;
      msg = dbEx.EntityValidationErrors.
        Aggregate(msg, (current1, validationResult) => validationResult.ValidationErrors.
          Aggregate(current1, (current, ve) => current + (sep + string.Format(msgFmt, ve.PropertyName, ve.ErrorMessage))));
      return new Exception(msg, dbEx);
    }
  }
}
