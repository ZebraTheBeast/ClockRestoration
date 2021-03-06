﻿using System;
using System.Collections.Generic;
using System.Web;

namespace ClockRestoration.ViewModels
{
    public class RequestOrderView
    {
        public long UserId { get; set; }
        public long DeliveryId { get; set; }
        public long PaymentId { get; set; }
        public long BrandId { get; set; }
        public long ClockTypeId { get; set; }
        public List<HttpPostedFileBase> Images { get; set; }
        public string Address { get; set; }
        public DateTime DeadLine { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
    }
}