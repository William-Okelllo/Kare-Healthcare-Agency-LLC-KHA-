using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Rpt
    {
        public int Id { get; set; }

        public int Plan_Id { get; set; }
        public DateTime AddedOn { get; set; }
        public string Staff { get; set; }
        public string Track { get; set; }

        public string Activity { get; set; }

        public string Goal { get; set; }


        public string Report { get; set; }
    }
}
