using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Reopen
    {
        public int Id { get; set; }

        public int Timesheet_id { get; set; }

        public DateTime CreatedOn { get; set; }
        public String Reason { get; set; }
        public int Status { get; set; }
        public String Department { get; set; }
        public String Staff { get; set; }
    }
}
