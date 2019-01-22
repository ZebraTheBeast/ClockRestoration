using System.Collections.Generic;

namespace ClockRestoration.ViewModels
{
    public class GetOrdersView
    {
        public List<OrderViewItem> Orders { get; set; }
        public int StatusId { get; set; }
    }
}
