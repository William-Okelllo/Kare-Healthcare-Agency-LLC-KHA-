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
    
    public partial class File
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Uploaded_By { get; set; }
        public string Access { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
        public string Category { get; set; }
        public string Agency { get; set; }
    }
}
