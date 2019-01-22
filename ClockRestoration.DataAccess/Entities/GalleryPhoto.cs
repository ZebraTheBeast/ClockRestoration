using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Entities
{
    public class GalleryPhoto : BaseEntity
    {
        public string ImageUrl { get; set; }

        public virtual Gallery Gallery { get; set; }
        public long GalleryId { get; set; }
    }
}
