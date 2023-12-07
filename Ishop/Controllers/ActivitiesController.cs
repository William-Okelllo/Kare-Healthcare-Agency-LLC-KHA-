using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class ActivitiesController : Controller
    {
        private Activities_Context db = new Activities_Context();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // GET: Activities/Create
        public ActionResult Add(DateTime Id)
        {

            ViewBag.Date = Id;

            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");


            DateTime currentDate = DateTime.Now;
            
            // Calculate the day of the week the month starts
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int daysPassed = (int)firstDayOfMonth.DayOfWeek;

            // Adjust daysPassed if the first day of the month is not Sunday (0)
            daysPassed = (daysPassed == 0) ? 0 : 7 - daysPassed;

            // Calculate the week number based on the starting day of the month
            int currentWeekNumber = GetCurrentWeekNumber(DateTime.Today);
            int weekNumber = currentWeekNumber;

            string weekInfo = $"{weekNumber:D2}{currentDate.Month:D2}{currentDate.Year}";
            string joinedStringConcat = string.Concat(weekInfo);
            int.TryParse(joinedStringConcat, out int WeekNo);

            ViewBag.Weekid = WeekNo;


            var Activities = db.activities.Where(a => a.WeekId == WeekNo && a.Day_Date ==Id ).ToList();
            ViewBag.Activities = Activities;
            var SumHours = db.activities.Where(c => c.WeekId == WeekNo && c.Day_Date == Id).Select(d => d.Hours).DefaultIfEmpty(0).Sum();
            var Chargable = db.activities.Where(c => c.WeekId == WeekNo && c.Day_Date == Id).Select(d => d.Charge).DefaultIfEmpty(0).Sum();
            ViewBag.SumHours = SumHours;
            ViewBag.Chargable = Chargable;
            return View();
        }


        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }


        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,CreatedOn,Project_Name,User,Name,Day_Date,Hours,direct_Comments,Indirect_Comments,Charge,Indirect,Indirect_Hours,WeekId")] Activities activities)
        {
            

            Employee_Context EE = new Employee_Context();
            var Emp = EE.employees.Where(c => c.Username == activities.User).FirstOrDefault();

            Decimal Charge = Emp.Rate * activities.Hours;
            activities.Charge = Charge;


            var SumHours = db.activities.Where(c => c.WeekId == activities.WeekId).Select(d => d.Hours + d.Indirect_Hours).DefaultIfEmpty(0).Sum();


            if (ModelState.IsValid)
            {
                db.activities.Add(activities);
                db.SaveChanges();
                Timesheet_Context Tc = new Timesheet_Context();
                var Timesheetid = Tc.timesheets.Where(t => t.Weekid == activities.WeekId).Select(x => x.Id).FirstOrDefault();
                Timesheet check = Tc.timesheets.Find(Timesheetid);
                if (check != null)
                {
                    check.Tt = check.Tt + activities.Indirect_Hours + activities.Hours;
                    Tc.SaveChanges();
                }

                return RedirectToAction("Index", "Timesheet");



            }

            return View(activities);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Project_Name,User,Name,TimesheetId,Hours,Comments")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activities);
        }

        // GET: Activities/Delete/5


        public ActionResult Delete(int id, string Day)
        {
            Activities activities = db.activities.Find(id);

            var Act = db.activities.Where(c => c.Id == id).Select(x => x.Hours);
            var TimesheetId = db.activities.Where(c => c.Id == id).FirstOrDefault();
            Timesheet_Context TC = new Timesheet_Context();




            if (TC != null)
            {
                // Use reflection to update the specified day's column
                var property = TC.GetType().GetProperty(Day, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    property.SetValue(TC, Act);

                    // Save the changes to the database
                    TC.SaveChanges();
                }
                else
                {
                    // Handle the case when the specified day doesn't exist as a column
                    // You can throw an exception, log an error, or handle it according to your needs.
                }
            }
            db.activities.Remove(activities);
            db.SaveChanges();
            string returnUrl = Request.UrlReferrer.ToString();
            TempData["msg"] = "✔   Activity dropped successfully ";
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
    }
}
