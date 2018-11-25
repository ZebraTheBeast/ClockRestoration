﻿using AutoMapper;
using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.DataAccess.Repositories;
using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace ClockRestoration.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IDeliveryRepository _deliveryRepository;
        private IPaymentRepository _paymentRepository;
        private IBrandRepository _brandRepository;
        private IClockTypeRepository _clockTypeRepository;

        public OrderService(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IPaymentRepository paymentRepository, IBrandRepository brandRepository, IClockTypeRepository clockTypeRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _paymentRepository = paymentRepository;
            _brandRepository = brandRepository;
            _clockTypeRepository = clockTypeRepository;
        }

        public ResponseOrderView GetInfoForOrder()
        {
            var responseOrderView = new ResponseOrderView();
            responseOrderView.Brands = Mapper.Map<List<Brand>, List<BrandViewItem>>(_brandRepository.GetAll());
            responseOrderView.ClockTypes = Mapper.Map<List<ClockType>, List<ClockTypeViewItem>>(_clockTypeRepository.GetAll());
            responseOrderView.Deliveries = Mapper.Map<List<Delivery>, List<DeliveryViewItem>>(_deliveryRepository.GetAll());
            responseOrderView.Payments = Mapper.Map<List<Payment>, List<PaymentViewItem>>(_paymentRepository.GetAll());

            return responseOrderView;
        }

        public void MakeOrder(RequestOrderView requestOrderView, string fileName)
        {
            var order = new Order();
            order = Mapper.Map<RequestOrderView, Order>(requestOrderView);

            order.ImageUrl = fileName;
            order.Status = OrderStatus.Pending;
            _orderRepository.Add(order);
        }

        public OrderViewItem GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            var orderView = OrderMap(order);
            return orderView;
        }

        public GetOrdersView GetOrders()
        {
            var getOrdersView = new GetOrdersView()
            {
                Orders = new List<OrderViewItem>()
            };

            var orders = _orderRepository.GetAll();

            foreach(var order in orders)
            {
                var orderView = OrderMap(order);
                getOrdersView.Orders.Add(orderView);
            }

            return getOrdersView;
        }

        public void UpdateOrderStatus(int id, OrderStatus status)
        {
            var order = _orderRepository.GetById(id);
            order.Status = status;
            _orderRepository.Update(order);
        }

        public void UpdateOrder(OrderViewItem orderViewItem)
        {
            var order = _orderRepository.GetById(orderViewItem.Id);
            order.Comments = orderViewItem.Comments;
            order.DeadLine = orderViewItem.DeadLine;
            order.Address = orderViewItem.Address;
            _orderRepository.Update(order);
        }

        private OrderViewItem OrderMap(Order order)
        {

            var status = (OrderStatus)order.Status;

            var orderView = new OrderViewItem
            {
                Address = order.Address,
                Brand = order.Brand.Title,
                ClockType = order.ClockType.Title,
                Comments = order.Comments,
                DeadLine = order.DeadLine.Date,
                Delivery = order.Delivery.Title,
                Id = order.Id,
                ImageUrl = order.ImageUrl,
                //Name = order.User.UserName,
                Payment = order.Payment.Title,
                Phone = order.PhoneNumber,
                Status = status.ToString()
            };

            return orderView;
        }


        public void Test()
        {
            var brand = new Brand
            {
                Title = "Brand 1"
            };

            var clockType = new ClockType
            {
                Title = "Clock Type 1"
            };

            var payment = new Payment
            {
                Title = "Payment 1"
            };

            var delivery = new Delivery
            {
                Title = "Delivery 1"
            };

            _brandRepository.Add(brand);
            _clockTypeRepository.Add(clockType);
            _paymentRepository.Add(payment);
            _deliveryRepository.Add(delivery);
        }
    }
}
