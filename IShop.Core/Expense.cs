using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Expense
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime AddedOn { get; set; }

        public string Track { get; set; }


        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}

