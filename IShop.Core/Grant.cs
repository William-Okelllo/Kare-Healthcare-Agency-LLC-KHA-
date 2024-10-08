using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Grant
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Display(Name = "Start Date")]
        public DateTime From_Date { get; set; }

        [Display(Name = "End Date")]
        public DateTime To_Date { get; set; }
        

        public string Type { get; set; }

        public string Track { get; set; }

        public string Status { get; set; }

        [Display(Name = "Added On")]
        public DateTime AddedOn { get; set; }

        [Display(Name = "Submitted By")]
        public string Submitted_by { get; set; }

        public Decimal Total { get; set; }
    }
}
