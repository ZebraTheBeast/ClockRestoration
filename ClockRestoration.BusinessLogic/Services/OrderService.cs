using AutoMapper;
using ClockRestoration.DataAccess.Repositories;
using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace ClockRestoration.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private OrderRepository _orderRepository;
        private DeliveryRepository _deliveryRepository;
        private PaymentRepository _paymentRepository;
        private BrandRepository _brandRepository;
        private ClockTypeRepository _clockTypeRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _deliveryRepository = new DeliveryRepository();
            _paymentRepository = new PaymentRepository();
            _brandRepository = new BrandRepository();
            _clockTypeRepository = new ClockTypeRepository();
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
                Name = order.User.UserName,
                Payment = order.Payment.Title,
                Phone = order.PhoneNumber,
                Status = status.ToString()
            };

            return orderView;
        }
    }
}
