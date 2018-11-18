using System;
using System.Data.Entity;

namespace ClockRestoration.Data.Context
{
    public interface IDataContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
    }
}
