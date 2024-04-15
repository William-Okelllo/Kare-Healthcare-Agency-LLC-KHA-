using Ishop.Infa;
using IShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Ishop.Controllers
{
    public class APIController : Controller
    {
        private leaveContext db = new leaveContext();
        // GET: API
        public ActionResult Index()
        {
            return View();
        }

        public void Updatetimesheetservice()
        {
            var leavewithbalances = db.leave.Where(c => c.Total_Hours > 0 && c.Status == 2).ToList();
            foreach (var leavesb in leavewithbalances)
            {
                IDays_leave_context dv = new IDays_leave_context();
                var Dayss = dv.days_Leaves.Where(l => l.Leave_Id == leavesb.Id).FirstOrDefault();


                // Adjust GetDatesInRange to exclude weekends
                List<DateTime> datesInRange = GetDatesInRangeWithoutWeekends(leavesb.From_Date, leavesb.To_Date);
                datesInRange = datesInRange.Where(date => date > DateTime.Now).ToList();
                Random random = new Random();
                int HoursOnleave;

                if (Dayss.Time == 1) { HoursOnleave = 8; } else { HoursOnleave = 4; }

                Timesheet_Context TT = new Timesheet_Context();
                foreach (var date in datesInRange)
                {
                    var Sheet = TT.timesheets.Where(i => i.Owner == leavesb.Employee && date >= i.From_Date && date <= i.End_Date).FirstOrDefault();
                    if (Sheet != null)
                    {
                        decimal addedHours = Sheet.Leave + HoursOnleave;

                        // Deduct added hours from leavee.Total_Hours


                        Sheet.Leave = addedHours;
                        Sheet.Tt = Sheet.Direct_Hours + Sheet.InDirect_Hours + Sheet.Leave;
                        TT.SaveChanges();

                        var leavee = db.leave.Where(c => c.Id == leavesb.Id).FirstOrDefault();
                        if (leavee != null)
                        {
                            leavee.Total_Hours = leavee.Total_Hours - HoursOnleave;
                            db.SaveChanges(); // Save changes for each iteration
                        }
                    }
                }
            }
        }

        public void Fixleaves()
        {
            var leavewithbalances = db.leave.Where(c => c.Total_Hours > 0 && c.Status == 2).ToList();
            foreach (var leavesb in leavewithbalances)
            {
                IDays_leave_context dv = new IDays_leave_context();
                var Dayss = dv.days_Leaves.Where(l => l.Leave_Id == leavesb.Id).FirstOrDefault();


                // Adjust GetDatesInRange to exclude weekends
                List<DateTime> datesInRange = GetDatesInRangeWithoutWeekends(leavesb.From_Date, leavesb.To_Date);

                Random random = new Random();
                int HoursOnleave;

                if (Dayss.Time == 1) { HoursOnleave = 8; } else { HoursOnleave = 4; }

                Timesheet_Context TT = new Timesheet_Context();
                foreach (var date in datesInRange)
                {
                    var Sheet = TT.timesheets.Where(i => i.Owner == leavesb.Employee && date >= i.From_Date && date <= i.End_Date).FirstOrDefault();
                    if (Sheet != null)
                    {
                        decimal addedHours = Sheet.Leave + HoursOnleave;

                        // Deduct added hours from leavee.Total_Hours


                        Sheet.Leave = addedHours;
                        Sheet.Tt = Sheet.Direct_Hours + Sheet.InDirect_Hours + Sheet.Leave;
                        TT.SaveChanges();

                        var leavee = db.leave.Where(c => c.Id == leavesb.Id).FirstOrDefault();
                        if (leavee != null)
                        {
                            leavee.Total_Hours = leavee.Total_Hours - HoursOnleave;
                            db.SaveChanges(); // Save changes for each iteration
                        }
                    }
                }
            }
        }








        // New method to get dates in range excluding weekends
        private List<DateTime> GetDatesInRangeWithoutWeekends(DateTime startDate, DateTime endDate)
        {
            List<DateTime> datesInRange = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Exclude weekends (Saturday and Sunday)
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    datesInRange.Add(date);
                }
            }

            return datesInRange;
        }





        public void Holidays()
        {
            Holiday_context HH = new Holiday_context();
            var currentDate = DateTime.Now.Date;
            var holidaysToday = HH.holidays.Where(c => c.Holiday_date == currentDate && c.Activated ==false).ToList();



            Employee_Context EE = new Employee_Context();
            var Emp = EE.employees.Where(c => c.Active == true).ToList();

            if (holidaysToday.Any())
            {
                foreach (var holiday in holidaysToday)
                {
                    using (var In_services = new Indirect_Activities_Context())
                    {
                        Random random = new Random();

                        foreach (var employee in Emp)
                        {
                            Indirect_Activities indirecttasks = new Indirect_Activities
                            {
                                Id = random.Next(),
                                User = employee.Username, // Assuming Id is the primary key of the employee table
                                Hours = holiday.Hours_Assigned,
                                Day_Date = holiday.Holiday_date,
                                CreatedOn = holiday.Holiday_date,
                                Comments = holiday.Holiday_Name,
                                Name = "WELLNESS & BREAKS",
                                WeekNo = GetCurrentWeekNumber(holiday.Holiday_date),
                                Approved = true
                            };

                            In_services.indirect_Activities.Add(indirecttasks);
                            In_services.SaveChanges();

                            Timesheet_Context TT = new Timesheet_Context();
                            var Sheet = TT.timesheets.Where(i => i.Owner == employee.Username && holiday.Holiday_date >= i.From_Date && holiday.Holiday_date <= i.End_Date).FirstOrDefault();
                            if (Sheet != null)
                            {
                                Sheet.Tt += holiday.Hours_Assigned;
                                Sheet.InDirect_Hours += holiday.Hours_Assigned;
                                TT.SaveChanges();
                            }
                        }

                        // Mark the holiday as processed
                        holiday.Activated = true;
                        HH.SaveChanges(); // Save changes to mark the holiday as processed
                    }

                }
            }
        }


       

        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }
    }
}