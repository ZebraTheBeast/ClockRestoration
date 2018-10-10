using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class ClockTypeRepository : GenericRepository<ClockType>, IClockTypeRepository
    {
        public ClockTypeRepository(string connectionString) : base(connectionString)
        {

        }
    }
}
