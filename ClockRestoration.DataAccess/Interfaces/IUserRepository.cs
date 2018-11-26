using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetByEmail(string email);
    }
}
