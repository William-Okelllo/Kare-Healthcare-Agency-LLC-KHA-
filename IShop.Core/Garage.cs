using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Garage
    {
        public int Id { get; set; }
        [Display(Name = "Garage Name")]
        public string Name { get; set; }

       
        public string location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int VAT { get; set; }
        public int Mark_Up { get; set; }

    }
}
