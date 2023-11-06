using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Phase
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [Display(Name = "Phase ")]
        public string Name { get; set; }

        public decimal Budget { get; set; }
    }
}
