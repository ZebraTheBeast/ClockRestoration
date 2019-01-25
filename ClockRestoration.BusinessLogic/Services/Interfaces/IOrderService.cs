using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System.Collections.Generic;

namespace ClockRestoration
{
    public interface IOrderService
    {
        ResponseOrderView GetInfoForOrder(string userName);
        void MakeOrder(RequestOrderView requestOrderView, List<string> filesName, string userName);
        OrderViewItem GetOrderById(int id);
        GetOrdersView GetOrders();
        GetOrdersView GetOrdersByStatus(OrderStatus status);
        void UpdateOrderStatus(int id, OrderStatus status);
        void UpdateOrder(OrderViewItem orderViewItem);
        void Test();
        string GetUserId(string userName);
        ResponseGalleryEditorView GetResponseGalleryEditorView();
        void AddGallery(string galleryName, string fileUrl);
        void DeleteGallery(long galleryId, string path);
        void EditGallery(EditGalleryView editGalleryView, string fileUrl, string path);
        ResponseGalleryDetailsView GetGallery(long galleryId);
        void AddPhotoToGallery(long galleryId, List<string> photosUrl);
        void DeletePhotoFromGallery(long photoId, string path);

        BrandEditorView GetAllBrands();
        void AddBrand(string brandTitle);
        void DeleteBrand(long brandId);
        void UpdateBrand(long brandId, string brandTitle);

        void CheckClocktype();

        DeliveryEditorView GetAllDelivery();
        void AddDelivery(string deliveryTitle);
        void DeleteDelivery(long deliveryId);
        void UpdateDelivery(long deliveryId, string deliveryTitle);

        PaymentEditorView GetAllPayment();
        void AddPayment(string paymentTitle);
        void DeletePayment(long paymentId);
        void UpdatePayment(long paymentId, string paymentTitle);

        ResponseCabinetView GetUserInfo(string userName);
        void UpdateUserInfo(RequestCabinetView requestCabinetView, string userName);

        GetOrdersView GetOrdersByUser(string userName);
    }
}
