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
            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;


            sp_expensess l9 = new sp_expensess();
            var boo9 = l9.sp_dash().ToList();
            ViewBag.l9 = boo9;




            return View();
        }
    

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}