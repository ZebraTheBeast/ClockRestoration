using ClockRestoration.Entities;
using System.Data.Entity;

namespace ClockRestoration.DataAccess.Context
{
    public class ClockRestorationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ClockType> ClockTypes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet <User> Users { get; set; }

        public ClockRestorationContext(string connectionString) : base(connectionString)
        {

        }
        public ClockRestorationContext() : base("Data Source=desktop-leu8p70;Initial Catalog=clockrestoration.mdf;Integrated Security=True")
        {

        }
    }
}
