using ClockRestoration.DataAccess.Context;
using ClockRestoration.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockRestoration.DataAccess.Repositories
{
    public class OrderRepository
    {
        private string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Order order)
        {
            using (ClockRestorationContext db = new ClockRestorationContext(_connectionString))
            {
                //db.Orders.Add(order);

                var payment = new Payment { Title = "zhopa" };
                db.Payments.Add(payment);

                db.SaveChanges();
            }
        }
    }
}
