using ClockRestoration.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace ClockRestoration.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;

        public AdminController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult CheckDb()
        {
            _orderService.CheckClocktype();
            return RedirectToAction("AdminPanel");
        }

        public ActionResult DeliveryEditor()
        {
            var model = _orderService.GetAllDelivery();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDelivery(string DeliveryTitle)
        {
            _orderService.AddDelivery(DeliveryTitle);
            return RedirectToAction("DeliveryEditor");
        }

        [HttpPost]
        public ActionResult EditDelivery(EditDeliveryView editDeliveryView)
        {
            _orderService.UpdateDelivery(editDeliveryView.DeliveryId, editDeliveryView.DeliveryTitle);
            return RedirectToAction("DeliveryEditor");
        }
        [HttpPost]
        public ActionResult DeleteDelivery(long DeliveryId)
        {
            _orderService.DeleteDelivery(DeliveryId);
            return RedirectToAction("DeliveryEditor");
        }
        //
        public ActionResult PaymentEditor()
        {
            var model = _orderService.GetAllPayment();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPayment(string PaymentTitle)
        {
            _orderService.AddPayment(PaymentTitle);
            return RedirectToAction("PaymentEditor");
        }

        [HttpPost]
        public ActionResult EditPayment(EditPaymentView editPaymentView)
        {
            _orderService.UpdatePayment(editPaymentView.PaymentId, editPaymentView.PaymentTitle);
            return RedirectToAction("PaymentEditor");
        }
        [HttpPost]
        public ActionResult DeletePayment(long PaymentId)
        {
            _orderService.DeletePayment(PaymentId);
            return RedirectToAction("PaymentEditor");
        }
        //
        public ActionResult BrandEditor()
        {
            var model = _orderService.GetAllBrands();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddBrand(string BrandTitle)
        {
            _orderService.AddBrand(BrandTitle);
            return RedirectToAction("BrandEditor");
        }

        [HttpPost]
        public ActionResult EditBrand(EditBrandView editBrandView)
        {
            _orderService.UpdateBrand(editBrandView.BrandId, editBrandView.BrandTitle);
            return RedirectToAction("BrandEditor");
        }
        [HttpPost]
        public ActionResult DeleteBrand(long BrandId)
        {
            _orderService.DeleteBrand(BrandId);
            return RedirectToAction("BrandEditor");
        }

        public ActionResult GalleryEditor()
        {
            var responseGalleryEditorView = _orderService.GetResponseGalleryEditorView();
            return View(responseGalleryEditorView);
        }

        
        public ActionResult DeletePhotoFromGallery(long photoId, long galleryId)
        {
            var path = Path.Combine(Server.MapPath("~/"));
            _orderService.DeletePhotoFromGallery(photoId, path);

            return RedirectToAction("GalleryDetails", new { galleryId = galleryId });
        }

        [HttpPost]
        public ActionResult AddPhotoToGallery(AddPhotoToGalleryView addPhotoToGalleryView)
        {

            var filesUrl = new List<string>();
            foreach (var image in addPhotoToGalleryView.Images)
            {
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Gallery/"), addPhotoToGalleryView.GalleryId.ToString(), fileName);
                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads/Gallery/"), addPhotoToGalleryView.GalleryId.ToString()));
                image.SaveAs(path);
                string fileUrl = $"Uploads/Gallery/{addPhotoToGalleryView.GalleryId}/{fileName}";
                filesUrl.Add(fileUrl);
            }
            _orderService.AddPhotoToGallery(addPhotoToGalleryView.GalleryId, filesUrl);

            return RedirectToAction("GalleryDetails", new { galleryId = addPhotoToGalleryView.GalleryId });
        }

        [HttpPost]
        public ActionResult EditGallery(EditGalleryView editGalleryView)
        {
            var fileUrl = string.Empty;

            if (editGalleryView.MainImage != null)
            {
                var folderId = Guid.NewGuid().ToString().Replace("-", "");
                var fileName = Path.GetFileName(editGalleryView.MainImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/Gallery/"), folderId, fileName);
                Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads/Gallery/"), folderId));
                editGalleryView.MainImage.SaveAs(path);
                fileUrl = $"Uploads/Gallery/{folderId}/{fileName}";
            }
            var pathToServer = Path.Combine(Server.MapPath("~/"));
            _orderService.EditGallery(editGalleryView, fileUrl, pathToServer);

            return RedirectToAction("GalleryDetails", new { galleryId = editGalleryView.GalleryId });
        }

        public ActionResult GalleryDetails(long galleryId)
        {
            var responseGalleryDetailsView = _orderService.GetGallery(galleryId);
            return View(responseGalleryDetailsView);
        }

        [HttpPost]
        public ActionResult DeleteGallery(RequestGalleryEditorView requestGalleryEditorView)
        {
            var path = Path.Combine(Server.MapPath("~/"));
            _orderService.DeleteGallery(requestGalleryEditorView.GalleryToEditId, path);

            return RedirectToAction("GalleryEditor");
        }


        [HttpPost]
        public ActionResult GalleryDetails(RequestGalleryEditorView requestGalleryEditorView)
        {
            var responseGalleryDetailsView = _orderService.GetGallery(requestGalleryEditorView.GalleryToEditId);

            return View(responseGalleryDetailsView);
        }

        [HttpPost]
        public ActionResult AddGallery(RequestGalleryEditorView requestGalleryEditorView)
        {
            var folderId = Guid.NewGuid().ToString().Replace("-", "");
            var fileName = Path.GetFileName(requestGalleryEditorView.MainImage.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/Gallery/"), folderId, fileName);
            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Uploads/Gallery/"), folderId));
            requestGalleryEditorView.MainImage.SaveAs(path);
            string fileUrl = $"Uploads/Gallery/{folderId}/{fileName}";

            _orderService.AddGallery(requestGalleryEditorView.GalleryName, fileUrl);

            return RedirectToAction("GalleryEditor");
        }
    }
}