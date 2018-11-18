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
            this._orderService = orderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            var responseOrderView = _orderService.GetInfoForOrder();
            return View(responseOrderView);
        }

        [HttpPost]
        public ActionResult MakeOrder(ResponseOrderView responseOrderView)
        {

            var requestOrderView = new RequestOrderView();
            requestOrderView = responseOrderView.Order;
            requestOrderView.UserId = 1;

            var fileId = Guid.NewGuid().ToString().Replace("-", "");
            var fileName = Path.GetFileName(requestOrderView.Image.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/Photo/"), requestOrderView.UserId.ToString(), fileId, fileName);
            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads/Photo/"), requestOrderView.UserId.ToString(), fileId));
            requestOrderView.Image.SaveAs(path);
            string fileUrl = $"Uploads/Photo/{requestOrderView.UserId.ToString()}/{fileId}/{fileName}";
            _orderService.MakeOrder(requestOrderView, fileUrl);

            return RedirectToAction("Index");
        }

        public ActionResult GetOrders()
        {
            var viewModel = _orderService.GetOrders();
            return View(viewModel);
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
    }
}