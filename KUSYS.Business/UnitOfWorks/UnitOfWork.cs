using KUSYS.Business.Caching.Base;
using KUSYS.Business.Caching.Redis.Server;
using KUSYS.Business.Caching.Redis.Service;
using KUSYS.Business.Repositories;
using KUSYS.Data.Caching;
using KUSYS.Database.DbContexts;
using KUSYS.Helper.WebHelpers;
using Microsoft.Extensions.Configuration;

namespace KUSYS.Business.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KUSYSDbContext _ctx;
        private readonly IConfiguration _configuration;
        private readonly RedisServer _redisServer;

        public UnitOfWork(IConfiguration configuration, KUSYSDbContext ctx, RedisServer redisServer)
        {
            _configuration = configuration;
            _ctx = ctx;
            _redisServer = redisServer;
        }


        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)Activator.CreateInstance(typeof(Repository<TEntity>), new object[] { _ctx });
        }

        public void Save()
        {
            using (var ctxTransaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    _ctx.SaveChanges();
                    ctxTransaction.Commit();
                }
                catch (Exception ex)
                {
                    ctxTransaction.Rollback();
                    while (ex.InnerException != null) ex = ex.InnerException;
                    throw new Exception(ex.Message);
                }
            }
        }

        //İstendiği kadar cache teknik ve servisi eklenebileceğinden bu yapı Reflection ile dinamikleştirildi.
        public ICacheService GetCacheService()
        {
            var technique = EnumHelper.ParseEnum<CacheTechnique>(_configuration.GetSection("CacheTechnique").Value);
            if (technique == CacheTechnique.Redis)
                return (ICacheService)Activator.CreateInstance(typeof(RedisCacheService), new object[] { _redisServer });
            else
                throw new NotImplementedException();
        }

    }
}
