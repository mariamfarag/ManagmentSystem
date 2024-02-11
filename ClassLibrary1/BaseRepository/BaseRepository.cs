using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic.BaseRepository
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private ManagementSystemDBContext _context = null;
        protected DbSet<TEntity> _dbSet;
        public BaseRepository(ManagementSystemDBContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public BaseRepository()
        {
            _context = new ManagementSystemDBContext();
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Exists(object key)
        {
            return _dbSet.Find(key) != null;
        }

        public IEnumerable<TEntity> GetByAll()
        {
            return _dbSet.ToList<TEntity>();
        }

        public IQueryable<TEntity> GetByAllQuery()
        {
            return _dbSet;
        }

        public TEntity GetById(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public void Remove(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void RemoveById(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Remove(entityToDelete);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            _context.Update(entityToUpdate);
            return entityToUpdate;
        }

        public void UpdateNew(TEntity entityToUpdate)
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _context.Attach(entityToUpdate);
            }
            _context.Update(entityToUpdate);
        }
    }
}
