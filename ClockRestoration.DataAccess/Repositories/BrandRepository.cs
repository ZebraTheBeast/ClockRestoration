using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository()
        {

        }
    }
}
