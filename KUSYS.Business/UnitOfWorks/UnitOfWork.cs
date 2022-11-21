using KUSYS.Business.Repositories;
using KUSYS.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly KUSYSDbContext _ctx;

        public UnitOfWork(KUSYSDbContext ctx)
        {
            _ctx = ctx;
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
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
