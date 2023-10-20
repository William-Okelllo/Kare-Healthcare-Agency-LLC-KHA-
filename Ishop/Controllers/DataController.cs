using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using Ishop.Models;
using System.Configuration;
using System.Data.SqlClient;
using PagedList;

namespace Ishop.Controllers
{ [Authorize]
    public class DataController : Controller
    {
        private Userstable db = new Userstable();

        // GET: Data
        public ActionResult Index(string searchBy, string search, int? page)
        {

            return View(db.AspNetUsers.ToList().ToPagedList(page ?? 1, 11));

        }



        // GET: Data/Details/5


        // GET: Data/Create
        public ActionResult Delete_User(Guid? id)
        {
            
            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("Delete_User", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", id);
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            TempData["msg"] = "✔ Account deleted successfully";

            return RedirectToAction("Index", "Data");
        }

        

       
       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
