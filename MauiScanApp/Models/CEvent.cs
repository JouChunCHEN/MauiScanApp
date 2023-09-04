using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiScanApp.Models
{
    public class CEvent
    {
        public int ProductDetail_ID { get; set; }
        public string ProductName { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Address { get; set; }
        public string ImageFileName { get; set; }
        public int Stock { get; set; }
    }
}
