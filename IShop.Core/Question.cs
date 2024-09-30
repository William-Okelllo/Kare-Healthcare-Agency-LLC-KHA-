using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Question
    {
        public int Id { get; set; }

        public int WorkPlan_Id { get; set; }

        public string Activity { get; set; }

        public string Goal { get; set; }

        [Display(Name = "Question")]
        public string Quiz { get; set; }
    }
}
