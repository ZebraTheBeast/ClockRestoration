using ClockRestoration.Entities;

namespace ClockRestoration.DataAccess.Entities
{
    public class OrderClockPhoto : BaseEntity
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }
    
        public string ImageUrl { get; set; }
    }
}
