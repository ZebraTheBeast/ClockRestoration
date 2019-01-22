using ClockRestoration.BusinessLogic.Services;
using ClockRestoration.Entities;
using ClockRestoration.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClockRestoration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login","Account" , new { });
            }
            var responseOrderView = _orderService.GetInfoForOrder(User.Identity.Name);
            return View(responseOrderView);
        }

        [HttpPost]
        public ActionResult MakeOrder(ResponseOrderView responseOrderView)
        {
            List<string> filesUrl = new List<string>();
            var requestOrderView = new RequestOrderView();

            var userId = _orderService.GetUserId(User.Identity.Name);

            requestOrderView = responseOrderView.Order;

            var folderId = Guid.NewGuid().ToString().Replace("-", "");
            foreach (var image in requestOrderView.Images)
            {
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Photo/"), userId, folderId, fileName);
                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads/Photo/"), userId, folderId));
                image.SaveAs(path);
                string fileUrl = $"Uploads/Photo/{userId}/{folderId}/{fileName}";
                filesUrl.Add(fileUrl);
            }
            
            _orderService.MakeOrder(requestOrderView, filesUrl, User.Identity.Name);

            return RedirectToAction("Index");
        }

        public ActionResult GetOrders()
        {
            var viewModel = _orderService.GetOrders();
            viewModel.StatusId = 0;
            return View(viewModel);
        }

        public ActionResult GetPendingOrders()
        {
            var viewModel = _orderService.GetOrdersByStatus(OrderStatus.Pending);
            viewModel.StatusId = 1;
            return View("GetOrders", viewModel);
        }

        public ActionResult GetInProgressOrders()
        {
            var viewModel = _orderService.GetOrdersByStatus(OrderStatus.InProgress);
            viewModel.StatusId = 2;
            return View("GetOrders", viewModel);
        }

        public ActionResult GetCompletedOrders()
        {
            var viewModel = _orderService.GetOrdersByStatus(OrderStatus.Completed);
            viewModel.StatusId = 3;
            return View("GetOrders", viewModel);
        }

        public ActionResult GetCanceledOrders()
        {
            var viewModel = _orderService.GetOrdersByStatus(OrderStatus.Canceled);
            viewModel.StatusId = 4;
            return View("GetOrders", viewModel);
        }


        public ActionResult Gallery()
        {
            var model = _orderService.GetResponseGalleryEditorView();
            return View(model);
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Cabinet()
        {
            var model = _orderService.GetUserInfo(User.Identity.Name);

            return View(model);
        }

        public ActionResult GetOrdersByUser()
        {
            var model = _orderService.GetOrdersByUser(User.Identity.Name);
            return View("GetOrders", model);
        }

        [HttpPost]
        public ActionResult EditUserInfo(RequestCabinetView requestCabinetView)
        {
            _orderService.UpdateUserInfo(requestCabinetView, User.Identity.Name);
            return RedirectToAction("Cabinet");
        }

        public ActionResult GalleryDetails(long galleryId)
        {
            var model = _orderService.GetGallery(galleryId);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var orderViewItem = _orderService.GetOrderById(id);
            return View(orderViewItem);
        }

        [HttpPost]
        public ActionResult Edit(OrderViewItem orderViewItem)
        {
            _orderService.UpdateOrder(orderViewItem);
            return RedirectToAction("Details", new { orderViewItem.Id });
        }

        public ActionResult Details(int id)
        {
            var orderViewItem = _orderService.GetOrderById(id);
            return View(orderViewItem);
        }

        public ActionResult ConfirmOrder(int id)
        {
            _orderService.UpdateOrderStatus(id, OrderStatus.InProgress);
            return RedirectToAction("Details", new { id });
        }

        public ActionResult DoneOrder(int id)
        {
            _orderService.UpdateOrderStatus(id, OrderStatus.Completed);
            return RedirectToAction("Details", new { id });
        }

        public ActionResult CancelOrder(int id)
        {
            _orderService.UpdateOrderStatus(id, OrderStatus.Canceled);
            return RedirectToAction("Details", new { id });
        }

        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}