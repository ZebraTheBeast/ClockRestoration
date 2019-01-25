using ClockRestoration.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ClockRestoration.DataAccess.Context
{
    public class ClockContextInitializer : DropCreateDatabaseIfModelChanges<ClockRestorationContext>
    {
        protected override void Seed(ClockRestorationContext context)
        {
            var adminRole = new IdentityRole();
            adminRole.Name = "admin";
            var userRole = new IdentityRole();
            userRole.Name = "user";

            var admin = new ApplicationUser();
            admin.UserName = "Admin";
            admin.FirstName = "Admin";
            admin.LastName = "Admin";
            admin.PhoneNumber = "+123123123";
            admin.Email = "admin@gmail.com";
            admin.PasswordHash = "PAssword123".GetHashCode().ToString();
            admin.Role = UserRole.Admin;

            var brand = new Brand();
            brand.Title = "Brand 1";

            var clockType = new ClockType();
            clockType.Title = "Clock Type 1";

            var payment = new Payment();
            payment.Title = "Payment 1";

            var delivery = new Delivery();
            delivery.Title = "Delivery 1";
            
            context.Payments.Add(payment);
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.Users.Add(admin);
            context.Brands.Add(brand);
            context.ClockTypes.Add(clockType);
            context.Deliveries.Add(delivery);

            context.SaveChanges();
        }
    }
}
