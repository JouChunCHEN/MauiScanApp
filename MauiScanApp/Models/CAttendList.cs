using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiScanApp.Models
{
    public class CAttendList
    {
        public int OrderDetailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string odNumber { get; set; }
        public bool? IsAttend { get; set; }
    }
}
