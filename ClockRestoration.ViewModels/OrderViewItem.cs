using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockRestoration.ViewModels
{
    public class OrderViewItem
    {
        public long Id { get; set; }
        public string Status { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime DeadLine { get; set; }

        public string Brand { get; set; }
        public string ClockType { get; set; }
        public List<string> ImagesUrl { get; set; }
        public string Comments { get; set; }

        public string Delivery { get; set; }
        public string Payment { get; set; }
        public decimal Cost { get; set; }
    }
}
