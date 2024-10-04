using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Purpose { get; set; }

        public Decimal Total { get; set; }
    }
}
