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
    public class HomeController : Controller
    {
        public ActionResult Index( FileUpload fileUpload)
        {

            

            HREntities l2 = new HREntities();
            var boo2 = l2.sp_invoices().ToList();
            ViewBag.l2 = boo2;

            HREntities l3 = new HREntities();
            var boo3 = l3.sp_expenses().ToList();
            ViewBag.l3 = boo3;

            leaves_t l7 = new leaves_t();
            var data12 = l7.leaves_Days_track.Where(c => c.Username == User.Identity.Name).ToList();
            ViewBag.F2 = data12;


            return View();
        }
        
        public ActionResult DashBoard(sp_dashboards sp_Dashboards ,string Staff, string Airline, string Service_Provider, string startDate, string endDate)
        {
            if(Staff ==null || Airline == null|| Service_Provider == null ||startDate == "" || endDate == "")
            {
               var  startDated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                var last = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                Staff = "All";
                Airline ="All";
                Service_Provider = "All";
                startDate = startDated.ToString();
                endDate =last.ToString();

            }
            sp_dashboards l3 = new sp_dashboards();
            var boo3 = l3.Dashboard_1(Staff, Airline, Service_Provider,startDate, endDate).ToList();
            ViewBag.l3 = boo3;

            sp_dashboards l4 = new sp_dashboards();
            var boo4 = l3.Dashboard_2(Staff, Airline, Service_Provider, startDate, endDate).ToList();
            ViewBag.l4 = boo4;

            sp_dashboards l5 = new sp_dashboards();
            var boo5 = l5.Dashboard_3(Staff, Airline, Service_Provider, startDate, endDate).ToList();
            ViewBag.l5 = boo5;

            sp_dashboards l6 = new sp_dashboards();
            var boo6 = l6.Dashboard_4(Staff, Airline, Service_Provider, startDate, endDate).ToList();
            ViewBag.l6 = boo6;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}