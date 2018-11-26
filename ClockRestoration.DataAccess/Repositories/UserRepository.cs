using ClockRestoration.DataAccess.Context;
using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;
using System.Data.Entity;
using System.Linq;

namespace ClockRestoration.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        DbSet<ApplicationUser> _dbSet;
        ClockRestorationContext _context;

        public UserRepository()
        {
            _context = new ClockRestorationContext();
            _dbSet = _context.Set<ApplicationUser>();
        }

        public ApplicationUser GetByEmail(string email)
        {
            var user = _dbSet.SingleOrDefault(u => u.Email == email);
            return user;
        }
    }
}
