using System.Collections.Generic;

namespace ClockRestoration.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetById(long id);
        List<TEntity> GetAll();
        void Delete(TEntity entity);
        void Update(TEntity entity);

    }
}
