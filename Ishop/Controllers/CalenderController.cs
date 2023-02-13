using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Ishop.Infa;
using Ishop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class CalenderController : Controller
    {
        // GET: Calender
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (ZO dc = new ZO())
            {
                var events = dc.GetShifts().Where(c => c.Status != "Deleted" ).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }



       








    [Authorize(Roles = "Admin")]
        public ActionResult All()
        {
            return View();
        }

        public JsonResult GetEvents2()
        {
            using (ZO dc = new ZO())
            {
                var events = dc.GetShifts().Where(c => c.Status != "Deleted").ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        [Authorize(Roles = "Facility")]
        public ActionResult Facility()
        {
           
            return View();
        }

        public JsonResult GetEvents9()
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
            using (ZO dc = new ZO())
            {
                var events = dc.GetShifts().Where(c => c.Status != "Deleted" && c.Created_by==AA).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [Authorize(Roles = "Staffing_Agency")]
        public ActionResult Staffing_Agency()
        {

            return View();
        }

        public JsonResult GetEvents10()
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
            using (ZO dc = new ZO())
            {
                var events = dc.GetShifts().Where(c => c.Status != "Deleted" && c.Staffing_Agency == AA).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [Authorize(Roles = "Can_Take_Shift")]
        public ActionResult Employees()
        {
            return View();
        }

        public JsonResult GetEvents3()
        {
            using (ZO dc = new ZO())
            {
                var events = dc.GetShifts().Where(c => c.Status != "Deleted" && c.Employee == User.Identity.Name).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }

}