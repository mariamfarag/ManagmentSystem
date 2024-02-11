using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {

        TEntity GetById(object id);
        IEnumerable<TEntity> GetByAll();
        IQueryable<TEntity> GetByAllQuery();
        TEntity Add(TEntity entity);
        void UpdateNew(TEntity entityToUpdate);
        TEntity Update(TEntity entityToUpdate);
        void RemoveById(object id);//delete
        void Remove(TEntity entityToDelete);
        bool Exists(object key);
        //---
        int Save();
        void Dispose();
    }
}
