using System.Collections.Generic;

namespace ClockRestoration.ViewModels
{
    public class ResponseOrderView
    {
        public List<DeliveryViewItem> Deliveries { get; set; }
        public List<PaymentViewItem> Payments { get; set; }
        public List<BrandViewItem> Brands { get; set; }
        public List<ClockTypeViewItem> ClockTypes { get; set; }

        public RequestOrderView Order { get; set; }

        public string UserPhoneNumber { get; set; }

        public ResponseOrderView()
        {
            Deliveries = new List<DeliveryViewItem>();
            Payments = new List<PaymentViewItem>();
            Brands = new List<BrandViewItem>();
            ClockTypes = new List<ClockTypeViewItem>();
        }
    }
}