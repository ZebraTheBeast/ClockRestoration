using System;

namespace ClockRestoration.Entities
{
    public class Order : BaseEntity
    {
        public virtual ApplicationUser User { get; set; }
        public long UserId { get; set; }

        public virtual Delivery Delivery { get; set; }
        public long DeliveryId { get; set; }

        public virtual Payment Payment { get; set; }
        public long PaymentId { get; set; }

        public virtual Brand Brand { get; set; }
        public long BrandId { get; set; }

        public virtual ClockType ClockType { get; set; }
        public long ClockTypeId { get; set; }

        public OrderStatus Status { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public DateTime DeadLine { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }

    }
}
