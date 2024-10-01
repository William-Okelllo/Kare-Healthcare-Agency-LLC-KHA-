using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Perform
    {
        public int Id { get; set; }

        public string Stage { get; set; }

        public string Subjects { get; set; }
        public string Student { get; set; }

        public string Grade { get; set; }
        public Decimal Percentage { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
    }

}
