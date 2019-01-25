using ClockRestoration.Entities;
using System.Collections.Generic;

namespace ClockRestoration.DataAccess.Entities
{
    public class Gallery : BaseEntity
    {
        public string Title { get; set; }
        public string MainImageUrl { get; set; }

        public virtual List<GalleryPhoto> GalleryPhotos { get; set; }
    }
}
