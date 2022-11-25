using KUSYS.Business.Caching.Base;
using KUSYS.Business.Repositories;

namespace KUSYS.Business.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Save();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        ICacheService GetCacheService();
    }
}
