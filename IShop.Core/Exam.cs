using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Exam
    {
        public int Id { get; set; }


        [Display(Name = "Exam name")]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Attendees { get; set; }

        public int defaulters { get; set; }

        public string Observations { get; set; }
    }
}
