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
    using System.ComponentModel.DataAnnotations;

    public partial class Project
    {
        public int id { get; set; }
        [Display(Name = "Project/Client Name")]
        public string Project1 { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}
