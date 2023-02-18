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



            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}