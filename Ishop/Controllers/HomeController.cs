using Ishop.Models;
using IShop.Core;
using Syncfusion.DocIO.DLS;
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
        public ActionResult Index()
        {
            HR2Entities ap =new HR2Entities();
            var Alp= ap.sp_TimesheetDash(User.Identity.Name).ToList();
            ViewBag.Alp = Alp;


            leaves_t l7 = new leaves_t();
            var data12 = l7.leaves_Days_track.Where(c => c.Username == User.Identity.Name).ToList();
            ViewBag.F2 = data12;


            return View();
        }
    

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}