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
        private DataContext db = new DataContext();

        // GET: Data
        public ActionResult Index(string searchBy, string search, int? page)
        {
            UL db = new UL();

            if(!(search == null))
            {
                return View(db.AspNetUsers.Where(c => c.UserName == search || c.UserName.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else 
            {
                return View(db.AspNetUsers.Where(c => c.UserName.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }
           

        }

        // GET: Data/Details/5


        // GET: Data/Create
        public ActionResult Delete_User(Guid? id)
        {
            uss db = new uss();
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
