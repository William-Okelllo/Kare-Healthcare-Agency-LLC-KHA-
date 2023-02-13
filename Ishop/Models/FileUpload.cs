using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Ishop.Models
{
    public class FileUpload
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Facility { get; set; }
        public string Username { get; set; }
        public string Category { get; set; }
        public string Uploaded_By { get; set; }
        public string Credential { get; set; }
        public DateTime UploadedOn { get; set; }
        public string Description { get; set; }
        public string Access { get; set; }
        public string longitude { get; set; }

        public int EmpId { get; set; }






        public string Phone { get; set; }
        public string Employee_Address { get; set; }
        public string Home_Address { get; set; }
        public string Emergency_Contact { get; set; }
        public string Resume_Path { get; set; }
        public string Certification_Path { get; set; }
        public string TB_Path { get; set; }
        public string Forms_ID_Path { get; set; }
        public string CPR_BLS_Path { get; set; }
        public string Training_Modules_Path { get; set; }

















        public string Agency { get; set; }
        public string latitude { get; set; }
        public IEnumerable<FileUpload> FileList { get; set; }


      
    }
}