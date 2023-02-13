using Ishop.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using File = Ishop.Models.File;

namespace Ishop.Controllers
{

    [Authorize]

    public class FilesController : Controller
    {






























        // GET: Files  
        public ActionResult Upload(FileUpload model, FormCollection form, HttpPostedFileBase file,int? id)
        {
            ZO r10 = new ZO();
            var data10 = r10.Em_Agency(User.Identity.Name).ToList();
            ViewBag.A10 = data10;

            ZO l2 = new ZO();
            var boo2 = l2.GetEmpUsername(id).ToList();
            ViewBag.l2 = boo2;



            string Description = form["Description"];
            List<FileUpload> list = new List<FileUpload>();
            DataTable dtFiles = GetFileDetails();
            foreach (DataRow dr in dtFiles.Rows)
            {
                list.Add(new FileUpload
                {
                    Id = dr["ID"].ToString(),
                    Name = dr["NAME"].ToString(),
                    Path = dr["PATH"].ToString(),
                    Uploaded_By = User.Identity.GetUserName(),
                    Access = form["Access"],
                    Agency = form["Agency"],
                    Description = model.Description,
                });
            }
            model.FileList = list;
            return View(model);
        }



        [HttpPost]
     
        public ActionResult Upload(FormCollection form, HttpPostedFileBase files ,int? id)
        {



            FileUpload model = new FileUpload();
            List<FileUpload> list = new List<FileUpload>();
            DataTable dtFiles = GetFileDetails();
            foreach (DataRow dr in dtFiles.Rows)
            {
                list.Add(new FileUpload
                {
                    Id = @dr["Id"].ToString(),
                    Name = @dr["NAME"].ToString(),
                    Path = @dr["PATH"].ToString(),
                    Uploaded_By = User.Identity.GetUserName(),
                    UploadedOn = DateTime.Now,
                    Description = form["Description"],
                    Access = form["Access"],
                    Category = form["Category"],
                    Agency = form["Agency"]
                   
                }); ;
            }
            model.FileList = list;

            string[] allowExtentions = (ConfigurationManager.AppSettings["FileType"]).Split(',');
            if (allowExtentions.Contains(Path.GetExtension(files.FileName)))
            {

                if (files != null)
                {
                    var Extension = Path.GetExtension(files.FileName);
                    var fileName = files.FileName + DateTime.Now.ToString("ss") + Extension;
                    string path = Path.Combine(Server.MapPath("~/Files"), fileName);
                    model.Path = Url.Content(Path.Combine("~/Files/", fileName));
                    model.Name = fileName;
                    model.Uploaded_By = User.Identity.GetUserName();
                    model.UploadedOn = DateTime.Now;
                    model.Description = form["Description"];
                    model.Access = form["Access"];
                    model.Agency = form["Agency"];
                    model.Category = form["Category"];





                    if (SaveFile(model))
                    {
                        files.SaveAs(path);
                        TempData["Msg"] = "Uploaded Successfully !!";
                        if (this.User.IsInRole("Admin"))
                        { return RedirectToAction("Employee_Files", "Employes", new { id = id }); }
                        else if (this.User.IsInRole("Staffing_Agency"))
                        { return RedirectToAction("Employee_Files", "Employes", new { id = id }); }
                        else
                        { return RedirectToAction("My_Files", "Files"); }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                        TempData["Msg"] = "Error In Add File. Please Try Again";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please Choose Correct File Type !!");
                    TempData["Msg"] = "Please Choose Correct File Type";
                    return View(model);
                }
                return RedirectToAction("Upload", "Files");
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                TempData["Msg"] = "Please Choose Correct File Type";
                return RedirectToAction("Upload", "Files");
            }
        }




        private DataTable GetFileDetails()
        {
            DataTable dtData = new DataTable();
            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            sqlCon.Open();
            SqlCommand command = new SqlCommand("Select * From files ", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            sqlCon.Close();
            return dtData;
        }

        private bool SaveFile(FileUpload model)
        {
            string strQry = "INSERT INTO Files (Name,Path,Uploaded_By,Description,UploadedOn,Access,Category,Agency) VALUES('" +
                model.Name + "','" + model.Path + "' ,'" + model.Uploaded_By + "' ,'" + model.Description + "' ,'" + model.UploadedOn + "' ,'" + model.Access + "' ,'" + model.Category + "' ,'" + model.Agency + "')";
            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand command = new SqlCommand(strQry, sqlCon);
            sqlCon.Open();
            int numResult = command.ExecuteNonQuery();
            sqlCon.Close();

            try
            {
                string strcon2 = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon2 = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("UpAcccess", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Path", model.Path);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (SqlException)
            {
                TempData["msg2"] = "✔ Error";
            }
            if (numResult > 0)
                return true;
            else
                return false;
        }
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }




        public ActionResult Index2(string searchBy, string search, int? page)
        {

            Files_list_ db = new Files_list_();

            if (searchBy == "Product")
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Name == search || search == null && c.Uploaded_By == User.Identity.Name).ToList().ToPagedList(page ?? 1, 9));

            }
            else
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Name.StartsWith(search) || search == null && c.Uploaded_By == User.Identity.Name).ToList().ToPagedList(page ?? 1, 9));

            }



        }
        public ActionResult My_Files(string searchBy, string search, int? page)
        {

            Files_list_ db = new Files_list_();

            if (searchBy == "Product")
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Access == User.Identity.Name).ToList().ToPagedList(page ?? 1, 9));

            }
            else
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Access == User.Identity.Name).ToList().ToPagedList(page ?? 1, 9));

            }



        }

        public ActionResult Delete_File(int? id)
        {
            Files_list_ db = new Files_list_();

            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand sqlcmnd = new SqlCommand("Delete_File", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@id", id);


            sqlCon.Open();
            sqlcmnd.ExecuteNonQuery();
            sqlCon.Close(); 
            TempData["Msg"] = "File delete successfully";
            return RedirectToAction("My_Files", "Files");
        }
        public ActionResult Emp_Files(string searchBy, string search, int? page)
        {
            string AA = null;

            string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.CommandText = "Em_Agency2";
                    cmd.Parameters.Add("@user", SqlDbType.NChar).Value = User.Identity.Name;


                    AA = (string)cmd.ExecuteScalar();

                    conn.Close();


                }



            }

            Files_list_ db = new Files_list_();

            if (!(search == null))
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Agency == AA && c.Access == search).ToList().ToPagedList(page ?? 1, 9));

            }
            else
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Agency == AA).ToList().ToPagedList(page ?? 1, 9));

            }



        }



    }

}