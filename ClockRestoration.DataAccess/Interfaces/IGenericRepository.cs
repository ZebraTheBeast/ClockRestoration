﻿using ClockRestoration.Entities;
using System.Collections.Generic;

namespace ClockRestoration.DataAccess.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity GetById(long id);
        List<TEntity> GetAll();
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> etities);
        void Update(TEntity entity);

    }
}
