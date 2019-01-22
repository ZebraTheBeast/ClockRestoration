using System.Collections.Generic;

namespace ClockRestoration.ViewModels
{
    public class ResponseGalleryDetailsView
    {
        public GalleryViewItem Gallery { get; set; }
        public List<GalleryPhotoViewItem> Photos { get; set; }
    }
}
