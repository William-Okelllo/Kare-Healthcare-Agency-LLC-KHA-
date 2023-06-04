using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Application
    {
        public int Id { get; set; }
        public int Job_id { get; set; }
        public string Job_Title { get; set; }     
        public DateTime Application_Date { get; set; }
        public string Applicant { get; set; }

        

    }
}
