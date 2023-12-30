using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ishop.Models
{
    public class TimesheetView
    {
        public int Week_Number { get; set; }
        public string Month_Name { get; set; }
        public decimal Direct_Hours { get; set; }
        public decimal InDirect_Hours { get; set; }
        public decimal Total_Hours { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }
}