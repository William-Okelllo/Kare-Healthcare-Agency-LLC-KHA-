using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Workplan
    {
        public int Id { get; set; }

        public string Track { get; set; }


        public string Goal { get; set; }

        public string Activities { get; set; }

        public string Guide { get; set; }

        public string Respondent { get; set; }
    }
}
