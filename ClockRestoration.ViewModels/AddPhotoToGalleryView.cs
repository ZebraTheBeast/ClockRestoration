using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClockRestoration.ViewModels
{
    public class AddPhotoToGalleryView
    {
        public long GalleryId { get; set; }
        public List<HttpPostedFileBase> Images { get; set; }
    }
}
