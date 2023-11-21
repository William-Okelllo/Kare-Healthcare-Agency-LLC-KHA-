using System;
using System.Collections.Generic;

namespace Ishop.Models
{
    public class FileUpload
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Facility { get; set; }
        public string Username { get; set; }
        public string Vehicle { get; set; }
        public string Vehicle_Reg { get; set; }
        public string Credential { get; set; }
        public DateTime UploadedOn { get; set; }
        public string Description { get; set; }
        public string Access { get; set; }
        public string longitude { get; set; }

        public int EmpId { get; set; }























        public string Agency { get; set; }
        public string latitude { get; set; }
        public IEnumerable<FileUpload> FileList { get; set; }



    }
}