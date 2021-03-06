﻿using ClockRestoration.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace ClockRestoration.Entities
{
    public class Order : BaseEntity
    {
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public virtual Delivery Delivery { get; set; }
        public long DeliveryId { get; set; }

        public virtual Payment Payment { get; set; }
        public long PaymentId { get; set; }

        public virtual Brand Brand { get; set; }
        public long BrandId { get; set; }

        public virtual ClockType ClockType { get; set; }
        public long ClockTypeId { get; set; }

        public virtual List<OrderClockPhoto> ClockPhotos { get; set; }

        public OrderStatus Status { get; set; }
        public string Address { get; set; }
        public DateTime DeadLine { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
        public decimal Cost { get; set; }

    }
}
