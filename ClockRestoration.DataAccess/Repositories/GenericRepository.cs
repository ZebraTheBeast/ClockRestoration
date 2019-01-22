using ClockRestoration.DataAccess.Context;
using ClockRestoration.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ClockRestoration.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> _dbSet;
        ClockRestorationContext _context;

        public GenericRepository()
        {
            _context = new ClockRestorationContext();
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange(List<TEntity> entities)
        {
            //_dbSet.RemoveRange(entities);
            foreach(var entity in entities)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public TEntity GetById(long id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
