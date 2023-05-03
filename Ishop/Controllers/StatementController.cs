using Ishop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class StatementController : Controller
    {
        // GET: Statement
        public ActionResult Index( string M )
        {


            sp_Charles_M l9 = new sp_Charles_M();
            var boo9 = l9.sp_monthly_statement(User.Identity.Name,M).ToList();
            ViewBag.l9 = boo9;





            sp_Charles_M dbb = new sp_Charles_M();
            var data10 = dbb.sp_Statements(User.Identity.Name).ToList();
            ViewBag.A10 = data10;



            sp_Charles_M l8 = new sp_Charles_M();
            var boo8 = l8.sp_statement_footer(User.Identity.Name,M).ToList();
            ViewBag.l8 = boo8;


            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
        }
        public ActionResult Statement(string M)
        {


            sp_Charles_M l9 = new sp_Charles_M();
            var boo9 = l9.sp_monthly_statement(User.Identity.Name, M).ToList();
            ViewBag.l9 = boo9;





            sp_Charles_M dbb = new sp_Charles_M();
            var data10 = dbb.sp_Statements(User.Identity.Name).ToList();
            ViewBag.A10 = data10;



            sp_Charles_M l8 = new sp_Charles_M();
            var boo8 = l8.sp_statement_footer(User.Identity.Name,M).ToList();
            ViewBag.l8 = boo8;


            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
        }
        public ActionResult Index2(string M)
        {


            sp_Charles_M l9 = new sp_Charles_M();
            var boo9 = l9.sp_monthly_statement(User.Identity.Name, M).ToList();
            ViewBag.l9 = boo9;





            sp_Income_proc dbb = new sp_Income_proc();
            var data10 = dbb.sp_Statements_Inc(User.Identity.Name).ToList();
            ViewBag.A10 = data10;


            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
        }
        public ActionResult Inc_Statement(string M)
        {


            sp_Income_proc l9 = new sp_Income_proc();
            var boo9 = l9.sp_monthly_statement_Inc(User.Identity.Name, M).ToList();
            ViewBag.l9 = boo9;





            sp_Income_proc dbb = new sp_Income_proc();
            var data10 = dbb.sp_Statements_Inc(User.Identity.Name).ToList();
            ViewBag.A10 = data10;





            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
        }
    }
}