using ClockRestoration.Entities;
using ClockRestoration.ViewModels;

namespace ClockRestoration
{
    public interface IOrderService
    {
        ResponseOrderView GetInfoForOrder();
        void MakeOrder(RequestOrderView requestOrderView, string fileName, string userName);
        OrderViewItem GetOrderById(int id);
        GetOrdersView GetOrders();
        void UpdateOrderStatus(int id, OrderStatus status);
        void UpdateOrder(OrderViewItem orderViewItem);
        void Test();
    }
}
