using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository()
        {

        }
    }
}
