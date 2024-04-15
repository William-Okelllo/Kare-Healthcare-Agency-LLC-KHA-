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
using PagedList;

namespace Ishop.Controllers
{
    [Authorize]
    public class HolidaysController : Controller
    {
        private Holiday_context db = new Holiday_context();

        // GET: Holidays
        public ActionResult Index(int? page)
        {
            return View(db.holidays.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));
        }

        // GET: Holidays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holiday holiday = db.holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // GET: Holidays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holidays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Holiday_date,Hours_Assigned,Activated,Holiday_Name")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.holidays.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(holiday);
        }

        // GET: Holidays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holiday holiday = db.holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: Holidays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Holiday_date,Hours_Assigned,Activated,Holiday_Name")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holiday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        // GET: Holidays/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Holiday holiday = db.holidays.Find(id);
            db.holidays.Remove(holiday);
            db.SaveChanges();
            TempData["msg"] = "Holiday dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Holidays_Pass(int id)
        {
            Holiday_context HH = new Holiday_context();
            var currentDate = DateTime.Now.Date;
            var holidaysToday = HH.holidays.Where(h => h.Id == id).ToList();

            


            Employee_Context EE = new Employee_Context();
            var Emp = EE.employees.Where(c => c.Active == true).ToList();


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

            TempData["msg"] = "Holiday assigned successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);

        }

        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }
    }
}
