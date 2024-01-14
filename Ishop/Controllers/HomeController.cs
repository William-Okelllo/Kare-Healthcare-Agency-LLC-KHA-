using Ishop.Infa;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {


           DepartmentContext DD = new DepartmentContext();
           var Depart = DD.departments.Where(d => d.Manager == User.Identity.Name).ToList();
           ViewBag.Departmentt = Depart;



            Direct_Activities_Context DA = new Direct_Activities_Context();
            Indirect_Activities_Context IDA = new Indirect_Activities_Context();
            DateTime currentDate = DateTime.Now.Date;
            var DirectA = DA.direct_Activities.Where(p => p.Day_Date == currentDate && p.User ==User.Identity.Name).Select(f=>f.Hours).DefaultIfEmpty(0).Sum();
            var InDirectA = IDA.indirect_Activities.Where(p => p.Day_Date == currentDate && p.User == User.Identity.Name).Select(f => f.Hours).DefaultIfEmpty(0).Sum();
            ViewBag.DirectHours = DirectA;
            ViewBag.InDirectHours = InDirectA;
            ViewBag.TotalHours = DirectA + InDirectA;


            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}