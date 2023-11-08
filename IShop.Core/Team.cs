using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Team
    {
        public int Id { get; set; }


        public int Project_id { get; set; }
        public string Project_Name { get; set; }
        [Display(Name = "Added on ")]
        public DateTime  CreatedOn { get; set; }
        public string Username { get; set; }
    }
}
