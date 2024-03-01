using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Notification
    {
        public int Id { get; set; }

        [Display(Name = "Created on")]
        public DateTime Createdon { get; set; }
        [Display(Name = "Last sent")]
        public DateTime Last_sent { get; set; }
        public bool Active { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
        public string Recepients { get; set; }
        public string GroupName { get; set; }
    }
}
