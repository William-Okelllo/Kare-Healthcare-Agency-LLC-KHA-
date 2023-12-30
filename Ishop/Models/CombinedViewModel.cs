using IShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ishop.Models
{
    public class CombinedViewModel
    {
        public int WeekNumber { get; set; }
        public string MonthName { get; set; }
        public string User { get; set; }
        public int IndirectHours { get; set; }
        public int DirectHours { get; set; }
        public int TotalHours { get; set; }
        public string Status { get; set; }
    }
}