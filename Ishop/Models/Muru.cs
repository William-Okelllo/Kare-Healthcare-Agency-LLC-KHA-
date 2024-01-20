using System;

namespace Ishop.Models
{
    public class Muru
    {
        public int Id { get; set; }
        public string Project_Name { get; set; }
        public string Phase { get; set; }
        public Decimal Budget { get; set; }
        public Decimal logged { get; set; }
        public Decimal Balance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime End_Date { get; set; }
    }
}