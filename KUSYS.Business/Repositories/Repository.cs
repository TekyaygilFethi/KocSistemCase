using KUSYS.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KUSYS.Business.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        KUSYSDbContext _ctx;

        public Repository(KUSYSDbContext ctx)
        {
            _ctx = ctx;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _ctx.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAllQueryable(bool isReadOnly = false)
        {
            IQueryable<T> query = null;
            if (isReadOnly)
                query = _ctx.Set<T>().AsNoTracking().AsQueryable();
            else
                query = _ctx.Set<T>().AsQueryable();

            return query;
        }

        public List<T> GetAllList(bool isReadOnly = false, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = null;
            if (isReadOnly)
                query = _ctx.Set<T>().AsNoTracking().AsQueryable();
            else
                query = _ctx.Set<T>().AsQueryable();


            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query.ToList();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query.Where(predicate);
        }

        public T Insert(T item)
        {
            _ctx.Set<T>().Add(item);
            return item;
        }

        public List<T> Insert(List<T> items)
        {
            _ctx.Set<T>().AddRange(items);
            return items;
        }

        public T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query.SingleOrDefault(predicate);
        }

        public void Attach(T item)
        {
            _ctx.Set<T>().Attach(item);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Any(predicate);
        }


        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
        }

        public int GetCount()
        {
            return _ctx.Set<T>().Count();
        }
    }
}
