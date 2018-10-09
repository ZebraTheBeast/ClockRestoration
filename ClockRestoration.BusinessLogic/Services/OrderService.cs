using ClockRestoration.DataAccess.Repositories;
using ClockRestoration.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockRestoration.BusinessLogic.Services
{
    public class OrderService
    {
        private OrderRepository _orderRepository;

        public OrderService(string connectionString)
        {
            _orderRepository = new OrderRepository(connectionString);
        }

        public void Test()
        {
            Order order = new Order();
            order.Comments = "Zhopa";

            _orderRepository.Add(order);
        }
    }
}
