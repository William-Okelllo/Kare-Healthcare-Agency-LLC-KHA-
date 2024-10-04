using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
	public class Exp
	{
		public int Id { get; set; }


        public int Budget_Id { get; set; }

        public string Type { get; set; }

		[Display(Name = "Budget Amount")]
		public Decimal Budget { get; set; }

		[Display(Name = "Total Expenditures")]
		public Decimal Expenditures { get; set; }

        [Display(Name = "Shared Buget")]
        public bool Shared { get; set; }
        public Decimal Available { get; set; }

	}
}
