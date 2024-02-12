using System;

namespace IShop.Core
{
    public class Employee
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Username { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }
        public String Contact { get; set; }
        public String DprtName { get; set; }
        public String Designation { get; set; }
        public String Userid { get; set; }
        public bool Active { get; set; }

        public decimal Rate { get; set; }

    }
}
