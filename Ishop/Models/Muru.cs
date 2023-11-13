using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ishop.Models
{
    public class Muru
    {
        public string Project_Name { get; set; }
        public string Phase { get; set; }
        public Decimal Budget { get; set; }
        public Decimal logged { get; set; }
        public Decimal Balance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime End_Date { get; set; }
    }
}   