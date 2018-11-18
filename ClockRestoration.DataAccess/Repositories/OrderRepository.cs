using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository()
        {
        }

    }
}
