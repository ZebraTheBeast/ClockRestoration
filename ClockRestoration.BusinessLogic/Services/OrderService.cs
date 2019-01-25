using AutoMapper;
using ClockRestoration.DataAccess.Entities;
using ClockRestoration.DataAccess.Interfaces;
using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System.Collections.Generic;

namespace ClockRestoration.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IDeliveryRepository _deliveryRepository;
        private IPaymentRepository _paymentRepository;
        private IBrandRepository _brandRepository;
        private IClockTypeRepository _clockTypeRepository;
        private IUserRepository _userRepository;
        private IOrderClockPhotoRepository _orderClockPhotoRepository;
        private IGalleryRepository _galleryRepository;
        private IGalleryPhotoRepository _galleryPhotoRepository;

        public OrderService(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IPaymentRepository paymentRepository, IBrandRepository brandRepository, IClockTypeRepository clockTypeRepository, IUserRepository userRepository, IOrderClockPhotoRepository orderClockPhotoRepository, IGalleryRepository galleryRepository, IGalleryPhotoRepository galleryPhotoRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _paymentRepository = paymentRepository;
            _brandRepository = brandRepository;
            _clockTypeRepository = clockTypeRepository;
            _userRepository = userRepository;
            _orderClockPhotoRepository = orderClockPhotoRepository;
            _galleryRepository = galleryRepository;
            _galleryPhotoRepository = galleryPhotoRepository;
        }

        public ResponseOrderView GetInfoForOrder(string userName)
        {
            var responseOrderView = new ResponseOrderView();
            responseOrderView.Brands = Mapper.Map<List<Brand>, List<BrandViewItem>>(_brandRepository.GetAll());
            responseOrderView.ClockTypes = Mapper.Map<List<ClockType>, List<ClockTypeViewItem>>(_clockTypeRepository.GetAll());
            responseOrderView.Deliveries = Mapper.Map<List<Delivery>, List<DeliveryViewItem>>(_deliveryRepository.GetAll());
            responseOrderView.Payments = Mapper.Map<List<Payment>, List<PaymentViewItem>>(_paymentRepository.GetAll());

            var user = _userRepository.GetByEmail(userName);
            responseOrderView.Order = new RequestOrderView();
            responseOrderView.Order.PhoneNumber = user.PhoneNumber;

            return responseOrderView;
        }

        public void MakeOrder(RequestOrderView requestOrderView, List<string> filesName, string userName)
        {
            var order = new Order();
            order = Mapper.Map<RequestOrderView, Order>(requestOrderView);

            var user = _userRepository.GetByEmail(userName);
            order.UserId = user.Id;
            order.Status = OrderStatus.Pending;
            order = _orderRepository.Add(order);

            foreach (var fileName in filesName)
            {
                var orderClockPhoto = new OrderClockPhoto();
                orderClockPhoto.OrderId = order.Id;
                orderClockPhoto.ImageUrl = fileName;

                _orderClockPhotoRepository.Add(orderClockPhoto);
            }
        }

        public string GetUserId(string userName)
        {
            var user = _userRepository.GetByEmail(userName);
            return user.Id;
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

            foreach (var order in orders)
            {
                var orderView = OrderMap(order);
                getOrdersView.Orders.Add(orderView);
            }

            return getOrdersView;
        }

        public GetOrdersView GetOrdersByStatus(OrderStatus status)
        {
            var getOrdersView = new GetOrdersView()
            {
                Orders = new List<OrderViewItem>()
            };

            var orders = _orderRepository.GetAll();

            foreach (var order in orders)
            {
                if (order.Status == status)
                {
                    var orderView = OrderMap(order);
                    getOrdersView.Orders.Add(orderView);
                }
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
            order.Cost = orderViewItem.Cost;
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
                Name = $"{order.User.FirstName} {order.User.LastName}",
                Payment = order.Payment.Title,
                Phone = order.PhoneNumber,
                Status = status.ToString(),
                ImagesUrl = new List<string>(),
                Cost = order.Cost
            };

            foreach (var clockPhoto in order.ClockPhotos)
            {
                orderView.ImagesUrl.Add(clockPhoto.ImageUrl);
            }

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

        public ResponseGalleryEditorView GetResponseGalleryEditorView()
        {
            var responseGalleryEditorView = new ResponseGalleryEditorView();
            responseGalleryEditorView.Galleries = new List<GalleryViewItem>();

            var galleries = _galleryRepository.GetAll();
            responseGalleryEditorView.Galleries = Mapper.Map<List<Gallery>, List<GalleryViewItem>>(galleries);

            return responseGalleryEditorView;
        }

        public void AddGallery(string galleryName, string fileUrl)
        {
            var gallery = new Gallery();
            gallery.Title = galleryName;
            gallery.MainImageUrl = fileUrl;

            _galleryRepository.Add(gallery);
        }

        public void DeleteGallery(long galleryId, string path)
        {
            var photos = _galleryPhotoRepository.GetAll();
            foreach(var photo in photos)
            {
                if(photo.GalleryId == galleryId)
                {
                    System.IO.File.Delete(path + photo.ImageUrl);
                    var newPhoto = _galleryPhotoRepository.GetById(photo.Id);
                    _galleryPhotoRepository.Delete(newPhoto);
                }
            }


            var gallery = _galleryRepository.GetById(galleryId);
            
            System.IO.File.Delete(path + gallery.MainImageUrl);

           
            _galleryRepository.Delete(gallery);
        }

        public void EditGallery(EditGalleryView editGalleryView, string fileUrl, string path)
        {
            var gallery = _galleryRepository.GetById(editGalleryView.GalleryId);
            gallery.Title = editGalleryView.GalleryTitle;
            if(!string.IsNullOrEmpty(fileUrl))
            {
                System.IO.File.Delete(path + gallery.MainImageUrl);
                gallery.MainImageUrl = fileUrl;
            }
            _galleryRepository.Update(gallery);
        }

        public ResponseGalleryDetailsView GetGallery(long galleryId)
        {
            var responseGalleryDetailsView = new ResponseGalleryDetailsView();

            responseGalleryDetailsView.Photos = new List<GalleryPhotoViewItem>();

            var gallery = _galleryRepository.GetById(galleryId);

            responseGalleryDetailsView.Gallery = Mapper.Map<Gallery, GalleryViewItem>(gallery);
            foreach(var photo in gallery.GalleryPhotos)
            {
                var galleryPhotoView = Mapper.Map<GalleryPhoto, GalleryPhotoViewItem>(photo);
                responseGalleryDetailsView.Photos.Add(galleryPhotoView);
            }

            return responseGalleryDetailsView;
        }

        public void AddPhotoToGallery(long galleryId, List<string> photosUrl)
        {
            foreach(var photoUrl in photosUrl)
            {
                var galleryPhoto = new GalleryPhoto();
                galleryPhoto.GalleryId = galleryId;
                galleryPhoto.ImageUrl = photoUrl;

                _galleryPhotoRepository.Add(galleryPhoto);
            }
        }

        public void DeletePhotoFromGallery(long photoId, string path)
        {
            var galleryPhoto = _galleryPhotoRepository.GetById(photoId);
            System.IO.File.Delete(path + galleryPhoto.ImageUrl);
            _galleryPhotoRepository.Delete(galleryPhoto);
        }

        public BrandEditorView GetAllBrands()
        {
            var brandEditorView = new BrandEditorView();
            brandEditorView.Brands = new List<BrandViewItem>();

            brandEditorView.Brands = Mapper.Map<List<Brand>, List<BrandViewItem>>(_brandRepository.GetAll());
            return brandEditorView;
        }

        public void AddBrand(string brandTitle)
        {
            var brand = new Brand();
            brand.Title = brandTitle;
            _brandRepository.Add(brand);
        }

        public void UpdateBrand(long brandId, string brandTitle)
        {
            var brand = _brandRepository.GetById(brandId);
            brand.Title = brandTitle;
            _brandRepository.Update(brand);
        }

        public void DeleteBrand(long brandId)
        {
            var brand = _brandRepository.GetById(brandId);
            _brandRepository.Delete(brand);
        }

        public void CheckClocktype()
        {
            var clocksType = _clockTypeRepository.GetAll();
            if(clocksType.Count != 3)
            {
                _clockTypeRepository.DeleteRange(clocksType);

                var clockType1 = new ClockType();
                clockType1.Title = "Наручные часы";

                var clockType2 = new ClockType();
                clockType2.Title = "Интерьерные часы";

                var clockType3 = new ClockType();
                clockType3.Title = "Карманные часы";

                _clockTypeRepository.Add(clockType1);
                _clockTypeRepository.Add(clockType2);
                _clockTypeRepository.Add(clockType3);
            }
        }

        public DeliveryEditorView GetAllDelivery()
        {
            var deliveryEditorView = new DeliveryEditorView();
            deliveryEditorView.Deliveries = new List<DeliveryViewItem>();

            deliveryEditorView.Deliveries = Mapper.Map<List<Delivery>, List<DeliveryViewItem>>(_deliveryRepository.GetAll());
            return deliveryEditorView;
        }

        public void AddDelivery(string deliveryTitle)
        {
            var delivery = new Delivery();
            delivery.Title = deliveryTitle;
            _deliveryRepository.Add(delivery);
        }

        public void DeleteDelivery(long deliveryId)
        {
            var delivery = _deliveryRepository.GetById(deliveryId);
            _deliveryRepository.Delete(delivery);
        }

        public void UpdateDelivery(long deliveryId, string deliveryTitle)
        {
            var delivery = _deliveryRepository.GetById(deliveryId);
            delivery.Title = deliveryTitle;
            _deliveryRepository.Update(delivery);
        }

        public PaymentEditorView GetAllPayment()
        {
            var paymentEditorView = new PaymentEditorView();
            paymentEditorView.Payments = new List<PaymentViewItem>();

            paymentEditorView.Payments = Mapper.Map<List<Payment>, List<PaymentViewItem>>(_paymentRepository.GetAll());
            return paymentEditorView;
        }

        public void AddPayment(string paymentTitle)
        {
            var payment = new Payment();
            payment.Title = paymentTitle;
            _paymentRepository.Add(payment);
        }

        public void DeletePayment(long paymentId)
        {
            var payment = _paymentRepository.GetById(paymentId);
            _paymentRepository.Delete(payment);
        }

        public void UpdatePayment(long paymentId, string paymentTitle)
        {
            var payment = _paymentRepository.GetById(paymentId);
            payment.Title = paymentTitle;
            _paymentRepository.Update(payment);
        }

        public ResponseCabinetView GetUserInfo(string userName)
        {
            var responseCabinetView = new ResponseCabinetView();
            var user = _userRepository.GetByEmail(userName);

            responseCabinetView.FirstName = user.FirstName;
            responseCabinetView.LastName = user.LastName;
            responseCabinetView.Email = userName;
            responseCabinetView.PhoneNumber = user.PhoneNumber;

            return responseCabinetView;
        }

        public void UpdateUserInfo(RequestCabinetView requestCabinetView, string userName)
        {
            var user = _userRepository.GetByEmail(userName);
            user.FirstName = requestCabinetView.FirstName;
            user.LastName = requestCabinetView.LastName;
            user.Email = requestCabinetView.Email;
            user.PhoneNumber = requestCabinetView.PhoneNumber;
            if(!string.IsNullOrEmpty(requestCabinetView.Password))
            {
                //change password
            }
            _userRepository.Update(user);
        }

        public GetOrdersView GetOrdersByUser(string userName)
        {
            var getOrdersView = new GetOrdersView();
            var user = _userRepository.GetByEmail(userName);
            getOrdersView.Orders = new List<OrderViewItem>();

            var orders = _orderRepository.GetAll();

            foreach (var order in orders)
            {
                if (order.UserId == user.Id)
                {
                    var orderView = OrderMap(order);
                    getOrdersView.Orders.Add(orderView);
                }
            }

            return getOrdersView;

        }
    }
}
