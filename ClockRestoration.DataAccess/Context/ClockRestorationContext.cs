﻿using ClockRestoration.Data.Context;
using ClockRestoration.DataAccess.Entities;
using ClockRestoration.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ClockRestoration.DataAccess.Context
{
    public class ClockRestorationContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ClockType> ClockTypes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<OrderClockPhoto> OrderClockPhotos { get; set; }

        public ClockRestorationContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<ClockRestorationContext>(new ClockContextInitializer());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
