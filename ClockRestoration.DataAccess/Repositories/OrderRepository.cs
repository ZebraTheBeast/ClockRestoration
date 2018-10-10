using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private string _connectionString;

        public OrderRepository(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
