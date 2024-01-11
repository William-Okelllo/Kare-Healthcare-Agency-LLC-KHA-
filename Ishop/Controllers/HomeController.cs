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


            Timesheet_Context context = new Timesheet_Context();

            // Get the integer value of the current day of the week (0 for Sunday, 1 for Monday, etc.)
            int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

            var Time = context.timesheets.Where(c => c.CreatedOn.Day == currentDayOfWeek);
            bool dataExists = Time.Any();

            if (dataExists)
            {
                ViewBag.Timesheet = "Todays' timesheet already submitted successfully";
            }
            else
            {
                if (currentDayOfWeek == 0)
                {
                    ViewBag.Timesheet = "Today's Sunday not mandatory to submit timesheet";
                }
                else
                {
                    ViewBag.Timesheet = "Todays' timesheet still pending for submission";
                }
            }


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