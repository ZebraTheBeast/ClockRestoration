using ClockRestoration.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ClockRestoration.Infrustructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IdentityFactoryOptions<ApplicationUserManager> options)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }
    }
}