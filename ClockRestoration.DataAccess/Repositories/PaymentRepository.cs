using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository()
        {

        }
    }
}
