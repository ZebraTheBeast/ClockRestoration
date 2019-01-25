using System.Web;

namespace ClockRestoration.ViewModels
{
    public class EditGalleryView
    {
        public long GalleryId { get; set; }
        public string GalleryTitle { get; set; }
        public HttpPostedFileBase MainImage { get; set; }
    }
}
