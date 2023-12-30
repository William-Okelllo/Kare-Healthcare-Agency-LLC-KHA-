using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Indirect_Activities
    {
        public int Id { get; set; }

        public int WeekNo { get; set; }
        public int Hours { get; set; }
        public DateTime Day_Date { get; set; }
        public DateTime CreatedOn { get; set; }
        

        public string User { get; set; }
        public string Comments { get; set; }

        [Display(Name = "Activity")]
        public string Name { get; set; }

        [Display(Name = "Approved ?")]
        public bool Approved { get; set; }

    }
}
