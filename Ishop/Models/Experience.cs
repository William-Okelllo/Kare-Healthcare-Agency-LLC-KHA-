//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ishop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Experience
    {
        public int id { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime? End_date { get; set; }
        public bool Present { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string App_user { get; set; }
    }
}