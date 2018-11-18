using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository()
        {

        }
    }
}
