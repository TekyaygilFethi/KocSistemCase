using KUSYS.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS.Business.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Save();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
