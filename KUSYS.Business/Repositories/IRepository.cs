using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllQueryable(bool isReadOnly = false);
        List<T> GetAllList(bool isReadOnly = false, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Single(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Insert(T item);
        List<T> Insert(List<T> items);
        void Attach(T item);
        bool Any(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
    }
}
