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
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Syncfusion.Pdf.Interactive;

namespace Ishop
{
    [Authorize]
    public class TimesheetController : Controller
    {
        private Timesheet_Context db = new Timesheet_Context();

        // GET: Timesheet
        public ActionResult Index(int? page)
        {


                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));
            

        }



        public JsonResult GetEvents()
        {
            using (Timesheet_Context dc = new Timesheet_Context())
            {
                var events = dc.timesheets.ToList().Where(c => c.Owner == User.Identity.Name).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }








        // GET: Timesheet/Details/5
        public ActionResult Details(int? id)
        {
            Activities_Context AA = new Activities_Context();
            var Activities = AA.activities.Where(a=>a.TimesheetId ==id).ToList();
            ViewBag.Activities = Activities;


            
            var SumHours = AA.activities.Where(c=>c.TimesheetId ==id).Select(d => d.Hours).DefaultIfEmpty(0).Sum();
            ViewBag.SumHours = SumHours;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }




        public ActionResult Submit(int Id)
        {
           
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 1;
                db.SaveChanges();
            }
            TempData["msg"] = "✔  Timesheet Submitted successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }









        // GET: Timesheet/Create
        public ActionResult Create()
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");


            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Owner,Weekid,Sun,Mon,Tue,Wen,Thur,Fri,Sat,Tt")] Timesheet timesheet, string action)
        {
           
            if (ModelState.IsValid)
            {
                db.timesheets.Add(timesheet);
                db.SaveChanges(); 
                TempData["msg"] = "✔   Timesheet successfully captured";
                return RedirectToAction("Details", "Timesheet", new { id = timesheet.Id });
            }

            return View(timesheet);
        }

        // GET: Timesheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Owner,Weekid,Sun,Mon,Tue,Wen,Thur,Fri,Sat")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timesheet);
        }

        // GET: Timesheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.timesheets.Find(id);
            db.timesheets.Remove(timesheet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InsertTimesheet()
        {
            

            
                try
                {
                    Employee_Context Emp = new Employee_Context();
                    List<Employee> employees = Emp.employees.ToList();

                    // Calculate the week number for the current date
                    int weekNumber = GetWeekNumber(DateTime.Now);

                    foreach (var employee in employees)
                    {
                        // Create a new Timesheet record
                        Timesheet timesheet = new Timesheet
                        {
                            CreatedOn = DateTime.Now,
                            Owner = employee.Username,
                            Weekid = weekNumber,
                            // Set other day-specific values here
                            Sun = 0,
                            Mon = 0,
                            Tue = 0,
                            Wen = 0,
                            Thur = 0,
                            Fri = 0,
                            Sat = 0,
                            Tt = 0,
                            Status = 0
                        };

                        // Insert the record into the "timesheet" table
                        db.timesheets.Add(timesheet);
                    }

                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            
           
        

        }
        private int GetWeekNumber(DateTime date)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            Calendar calendar = ci.Calendar;

            // Determine the first day of the year and the first day of the week
            DateTimeFormatInfo dfi = ci.DateTimeFormat;
            DateTime jan1 = new DateTime(date.Year, 1, 1);
            DayOfWeek jan1DayOfWeek = dfi.Calendar.GetDayOfWeek(jan1);

            // Adjust the start of the week based on the specified calendar
            int daysToFirstWeek = 7 - (int)jan1DayOfWeek;
            int daysToFirstWeekIncl = daysToFirstWeek - 1;

            // Calculate the day number of the current date
            int dayOfYear = calendar.GetDayOfYear(date);

            if (dayOfYear <= daysToFirstWeekIncl)
            {
                // The current date is in the first week of the year
                return 1;
            }

            // Calculate the week number
            int weekNumber = (dayOfYear - daysToFirstWeekIncl + 6) / 7;
            return weekNumber;
        }

       
    }
}
