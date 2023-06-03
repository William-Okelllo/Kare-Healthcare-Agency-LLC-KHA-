using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Profile
    {
        public int Id { get; set; }


        public string email { get; set; }
        public int   Phone { get; set; }
        public string sector { get; set; }
        public string Residency { get; set; }
        public string Fullnames { get; set; }
        public string Gender { get; set; }
        [Display(Name = "last update")]
        public DateTime lastupdate { get; set; }
    }
}
