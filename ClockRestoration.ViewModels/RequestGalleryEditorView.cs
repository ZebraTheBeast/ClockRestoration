using System.Web;

namespace ClockRestoration.ViewModels
{
    public class RequestGalleryEditorView
    {
        public long GalleryToEditId{ get; set; }
        public HttpPostedFileBase MainImage { get; set; }
        public string GalleryName { get; set; }
    }
}
